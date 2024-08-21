using Application.Interfaces.Repositories;
using Interconnection.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Interconnection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInterconnectionLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSingleton<IGithubRepository, GithubRepository>();
            return services;
        }
    }
}
