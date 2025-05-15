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
    public class PostUsuarioCmd : IRequest<bool>
    {
        public int? IdUsuario { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Activo { get; set; }
        public int IdPerfil { get; set; }
        public class PostUsuarioNuevoHandler : IRequestHandler<PostUsuarioCmd, bool>
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
            }
            public async Task<bool> Handle(PostUsuarioCmd rq, CancellationToken cancellationToken)
            {
                try
                {
                    if (rq.IdUsuario != null)
                    {
                        var usuario = await _dataEstudiantesDbContext.UsuariosDbEntity.FindAsync(rq.IdUsuario);
                        usuario.Cedula = rq.Cedula;
                        usuario.Nombre = rq.Nombre;
                        usuario.Email = rq.Email;
                        usuario.Direccion = rq.Direccion;
                        usuario.Telefono = rq.Telefono;
                        usuario.FechaNacimiento = rq.FechaNacimiento;
                        usuario.Activo = rq.Activo;

                        var perfil = await _dataEstudiantesDbContext.UsuarioPerfilDbEntity
                              .Where(p => p.IdUsuario == rq.IdUsuario)
                              .ProjectTo<UsuarioPerfilEntity>(_mapper.ConfigurationProvider)
                              .AsNoTracking()
                              .FirstAsync(cancellationToken);

                        perfil.IdPerfil = rq.IdPerfil;

                        _dataEstudiantesDbContext.UsuarioPerfilDbEntity.Update(perfil);

                        var rlt = await _dataEstudiantesDbContext.SaveChangesAsync(cancellationToken);
                        return rlt == 1 ? true : false;
                    }
                    else
                    {
                        EncryptionFactory encrypt = new EncryptionFactory();
                        string encryptedKey = encrypt.Encrypt(rq.Password, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);
                        var entity = new UsuariosEntity
                        {

                            Cedula = rq.Cedula,
                            Nombre = rq.Nombre,
                            Username = rq.Username,
                            Password = encryptedKey,
                            Email = rq.Email,
                            Direccion = rq.Direccion,
                            Telefono = rq.Telefono,
                            FechaNacimiento = rq.FechaNacimiento,
                            Activo = rq.Activo
                        };

                        _dataEstudiantesDbContext.UsuariosNuevoEntity.Add(entity);

                        await _dataEstudiantesDbContext.SaveChangesAsync(cancellationToken);
                        var entityPerfil = new UsuarioPerfilEntity
                        {
                            IdUsuario = entity.IdUsuario,
                            IdPerfil = rq.IdPerfil
                        };
                        _dataEstudiantesDbContext.UsuarioPerfilDbEntity.Add(entityPerfil);
                        await _dataEstudiantesDbContext.SaveChangesAsync(cancellationToken);

                        return entity.IdUsuario > 0 ? true : false;

                    }

                }
                catch (Exception e)
                {

                    throw null;
                }
            }
        }
    }
}
