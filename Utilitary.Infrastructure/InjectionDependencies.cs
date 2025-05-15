namespace Utilitary.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using System.Data.SqlClient;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Infrastructure.Persistence;

    public static class InjectionDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
          

            services.AddDbContext<DataEstudiantesDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EstudiantesData")));
            services.AddScoped<IDataEstudiantesDbContext>(provider => provider.GetService<DataEstudiantesDbContext>());

            services.AddTransient<IProceduresDataEstudiantes, ProceduresDataEstudiantes>();

            return services;
        }
    }
}
