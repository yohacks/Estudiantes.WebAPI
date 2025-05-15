namespace Utilitary.Infrastructure.Persistence.Configuration.Administracion
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Utilitary.Domine;
    class UsuarioPerfilCfg : IEntityTypeConfiguration<UsuarioPerfilEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfilEntity> builder)
        {
            builder.ToTable("SEG_UsuarioPerfil", "dbo")
            .HasKey(x => x.Id);
        }
    }
}
