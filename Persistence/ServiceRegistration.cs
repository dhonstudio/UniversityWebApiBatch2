using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSqlServer<SchoolContext>(configuration.GetConnectionString("Training"),
                x => x.MigrationsHistoryTable("_migrationsHistory", "ripas"));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IStudentRepository, StudentRepo>();

            return services;
        }
    }
}
