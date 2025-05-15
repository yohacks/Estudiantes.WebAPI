namespace Utilitary.Core.Administracion.Queries
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Mmbari.Encripcion;
    using Newtonsoft.Json;
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

    public class GetUserPasswordQry : IRequest<string>
    {
        public string Username { get; set; }

        public class GetPasswordUserHandler : IRequestHandler<GetUserPasswordQry, string>
        {
            private readonly IConfiguration _configuration;
            private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;
            private readonly InternalServices _internalServices;

            public GetPasswordUserHandler(IConfiguration configuration, IProceduresDataEstudiantes proceduresDataEstudiantes, InternalServices internalServices)
            {
                _configuration = configuration;
                _proceduresDataEstudiantes = proceduresDataEstudiantes;
                _internalServices = internalServices;
            }

            public async Task<string> Handle(GetUserPasswordQry rq, CancellationToken cancellationToken)
            {
                try
                {
                    string desencryptedKey = "No se encontraron registros en la Base de Datos.";

                    // Consulta la información del usuario por el Username
                    List<Parameters> ParametrosSp = new List<Parameters>
                    {
                        new Parameters { ParameterName = "@Username", Type = DbType.String, ParameterValue = rq.Username }
                    };

                    UsuariosEntity UserInfo = new UsuariosEntity();
                    UserInfo = await _proceduresDataEstudiantes.ExecuteFirstOrDefaultSpAsync<UsuariosEntity>("dbo.PRP_SEG_Usuarios", ParametrosSp, cancellationToken);

                    if (UserInfo != null)
                    {
                        EncryptionFactory encrypt = new EncryptionFactory();
                        desencryptedKey = encrypt.Dencrypt(UserInfo.Password, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);

                    }
                    return desencryptedKey;

                }
                catch (Exception ex)
                {
                    _internalServices.SaveErrorLog("ERROR GetUserPasswordQry", ex, rq.Username);
                    throw new NotFoundException(nameof(GetUserPasswordQry), ex.Message);
                }
            }
        }
    }
}