using Application.Features.AuthFeature;
using Application.Features.StudentFeature;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<StudentFeature>();
            services.AddScoped<AuthFeature>();
            return services;
        }
    }
}
