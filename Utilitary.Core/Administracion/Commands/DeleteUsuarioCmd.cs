namespace Utilitary.Core.Administracion.Commands
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Mmbari.Encripcion;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Core.Common.Utilities;
    using Utilitary.Domine;
    public class DeleteUsuarioCmd : IRequest<bool>
    {
        public int IdUsuario { get; set; }

        public class PostUsuarioNuevoHandler : IRequestHandler<DeleteUsuarioCmd, bool>
        {
            private readonly IDataEstudiantesDbContext _dataEstudiantesDbContext;
            public readonly IMapper _mapper; private readonly IConfiguration _configuration;
            private readonly InternalServices _internalService;
            public PostUsuarioNuevoHandler(IDataEstudiantesDbContext dataEstudiantesDbContext, IConfiguration configuration, InternalServices internalService, IMapper mapper)
            {
                _internalService = internalService;
                _configuration = configuration;
                _dataEstudiantesDbContext = dataEstudiantesDbContext;
                _mapper = mapper;
                _mapper = mapper;
            }
            public async Task<bool> Handle(DeleteUsuarioCmd rq, CancellationToken cancellationToken)
            {
                try
                {
                    // Obtener el usuario
                    var usuario = await _dataEstudiantesDbContext.UsuariosDbEntity
                        .FindAsync(new object[] { rq.IdUsuario }, cancellationToken);

                    if (usuario == null)
                        return false;

                    // Eliminar el perfil del usuario (si existe)
                    var perfiles = await _dataEstudiantesDbContext.UsuarioPerfilDbEntity
                        .Where(p => p.IdUsuario == rq.IdUsuario)
                        .ToListAsync(cancellationToken);

                    if (perfiles.Any())
                    {
                        _dataEstudiantesDbContext.UsuarioPerfilDbEntity.RemoveRange(perfiles);
                    }

                    // Eliminar el usuario
                    _dataEstudiantesDbContext.UsuariosDbEntity.Remove(usuario);

                    // Guardar los cambios
                    var rlt = await _dataEstudiantesDbContext.SaveChangesAsync(cancellationToken);
                    return rlt > 0;

                }
                catch (Exception e)
                {

                    throw null;
                }
            }
        }
    }
}
