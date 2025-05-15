namespace Utilitary.Infrastructure.Persistence.Configuration.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Utilitary.Domine.Common;

    class TemplateMailCfg : IEntityTypeConfiguration<TemplateMail>
    {
        public void Configure(EntityTypeBuilder<TemplateMail> builder)
        {
            builder.ToTable("SYS_Template_Mail", "dbo")
            .HasKey(x => x.Id);
        }
    }
}