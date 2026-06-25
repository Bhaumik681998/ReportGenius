using Microsoft.Extensions.DependencyInjection;
using ReportGenius.Application.Services;

namespace ReportGenius.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Future Service Registrations

            services.AddScoped<DatabaseService>();

            return services;
        }
    }
}
