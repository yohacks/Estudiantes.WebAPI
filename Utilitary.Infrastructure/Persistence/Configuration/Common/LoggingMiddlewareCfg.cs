namespace Utilitary.Infrastructure.Persistence.Configuration.Common
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Utilitary.Domine.Common;

    class LoggingMiddlewareCfg : IEntityTypeConfiguration<LogMiddleware>
    {
        public void Configure(EntityTypeBuilder<LogMiddleware> builder)
        {
            builder.ToTable("SYS_Logging", "dbo")
            .HasKey(x => x.Id);

        }
    }
}
