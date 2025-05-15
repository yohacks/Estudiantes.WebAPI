namespace Utilitary.Infrastructure.Persistence.Configuration.Administracion
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Utilitary.Domine;
    class UsuarioCfg : IEntityTypeConfiguration<UsuariosEntity>
    {
        public void Configure(EntityTypeBuilder<UsuariosEntity> builder)
        {
            builder.ToTable("SEG_Usuarios", "dbo")
            .HasKey(x => x.IdUsuario);
        }
    }
}

