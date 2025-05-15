namespace Utilitary.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public class DataEstudiantesDbContext : DbContext, IDataEstudiantesDbContext
    {
        public DataEstudiantesDbContext(DbContextOptions<DataEstudiantesDbContext> options) : base(options)
        {
        }
        public DbSet<UsuariosEntity> UsuariosDbEntity { get; set; }
        public DbSet<PerfilEntity> PerfilDbEntity { get; set; }
        public DbSet<UsuariosEntity> UsuariosNuevoEntity { get; set; }
        public DbSet<UsuarioPerfilEntity> UsuarioPerfilDbEntity { get; set; }
        public DbSet<TemplateMail> TemplateMailDbEntity { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataEstudiantesDbContext).Assembly);
        }
    }
}
