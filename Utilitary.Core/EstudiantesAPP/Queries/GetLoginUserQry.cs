namespace Utilitary.Core.Estudiantes.Queries
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Mmbari.Encripcion;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Exceptions;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Core.Common.Utilities;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public class GetLoginUserQry : IRequest<UsuariosModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class LoginHandler : IRequestHandler<GetLoginUserQry, UsuariosModel>
        {
            private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;
            private readonly IConfiguration _configuration;
            private readonly InternalServices _internalService;

            public LoginHandler(IProceduresDataEstudiantes proceduresDataEstudiantes, IConfiguration configuration, InternalServices internalService)
            {
                _proceduresDataEstudiantes = proceduresDataEstudiantes;
                _configuration = configuration;
                _internalService = internalService;
            }

            public async Task<UsuariosModel> Handle(GetLoginUserQry rq, CancellationToken cancellationToken)
            {
                try
                {
                    EncryptionFactory encrypt = new EncryptionFactory();
                    string encryptedKey = encrypt.Encrypt(rq.Password, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);

                    List<Parameters> ParametrosSp = new List<Parameters> {
                    new Parameters { ParameterName = "@Usuario", Type = DbType.String, ParameterValue = rq.Username },
                    new Parameters { ParameterName = "@Clave", Type = DbType.String, ParameterValue = encryptedKey }
                    };

                    UsuariosModel rsltUser = await _proceduresDataEstudiantes.ExecuteFirstOrDefaultSpAsync<UsuariosModel>("dbo.PRC_SEG_Login", ParametrosSp, cancellationToken);

                    if (rsltUser != null)
                    {
                        List<Parameters> ParamSpUser = new List<Parameters>
                    {
                        new Parameters { ParameterName = "@IdUsuario", Type = DbType.String, ParameterValue = rsltUser.IdUsuario },
                    };

                        rsltUser = await _proceduresDataEstudiantes.ExecuteFirstOrDefaultSpAsync<UsuariosModel>("dbo.PRC_SEG_UsuarioID", ParamSpUser, cancellationToken);
                    }
                    return rsltUser;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR GetLoginUserQry", ex);
                    throw new NotFoundException(nameof(GetLoginUserQry), ex.Message);
                }
            }
        }
    }
}