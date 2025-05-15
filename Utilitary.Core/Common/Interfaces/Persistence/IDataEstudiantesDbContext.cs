namespace Utilitary.Core.Common.Interfaces.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public interface IDataEstudiantesDbContext
    {
        public DbSet<UsuariosEntity> UsuariosDbEntity { get; set; }
        public DbSet<PerfilEntity> PerfilDbEntity { get; set; }
        public DbSet<UsuariosEntity> UsuariosNuevoEntity { get; set; }
        public DbSet<UsuarioPerfilEntity> UsuarioPerfilDbEntity { get; set; }
        DbSet<TemplateMail> TemplateMailDbEntity { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
