namespace Utilitary.Core.Estudiantes.Commands
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Mmbari.Encripcion;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Exceptions;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Core.Common.Utilities;

    public class UpdatePasswordCmd : IRequest<bool>
    {
        public int IdUsuario { get; set; }
        public string ContraseñaActual { get; set; }
        public string ContraseñaNueva { get; set; }

        public class UpdateActividadesHandler : IRequestHandler<UpdatePasswordCmd, bool>
        {
            private readonly IDataEstudiantesDbContext _DataEstudiantesDbContext;
            private readonly IConfiguration _configuration;
            private readonly InternalServices _internalService;

            public UpdateActividadesHandler(IDataEstudiantesDbContext DataEstudiantesDbContext, IConfiguration configuration, InternalServices internalService)
            {
                _DataEstudiantesDbContext = DataEstudiantesDbContext;
                _configuration = configuration;
                _internalService = internalService;
            }
            public async Task<bool> Handle(UpdatePasswordCmd rq, CancellationToken cancellationToken)
            {
                try
                {
                    Boolean result = false;
                    var entity = await _DataEstudiantesDbContext.UsuariosDbEntity.FindAsync(rq.IdUsuario);

                    if (entity == null)
                        throw new NotFoundException(nameof(UpdatePasswordCmd), rq.IdUsuario);

                    EncryptionFactory encrypt = new EncryptionFactory();
                    string ContraseñaActual = encrypt.Encrypt(rq.ContraseñaActual, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);

                    if (ContraseñaActual == entity.Password)
                    {
                        string ContraseñaNueva = encrypt.Encrypt(rq.ContraseñaNueva, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);

                        entity.Password = ContraseñaNueva;
                        await _DataEstudiantesDbContext.SaveChangesAsync(cancellationToken);
                        result = true;
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR UpdatePasswordCmd", ex);
                    throw new NotFoundException(nameof(UpdatePasswordCmd), ex);
                }
            }
        }
    }
}