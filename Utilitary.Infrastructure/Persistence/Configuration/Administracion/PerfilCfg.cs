namespace Utilitary.Infrastructure.Persistence.Configuration.Administracion
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Utilitary.Domine;
    class PerfilCfg : IEntityTypeConfiguration<PerfilEntity>
    {
        public void Configure(EntityTypeBuilder<PerfilEntity> builder)
        {
            builder.ToTable("SEG_Perfiles", "dbo")
            .HasKey(x => x.IdPerfil);
        }
    }
}
