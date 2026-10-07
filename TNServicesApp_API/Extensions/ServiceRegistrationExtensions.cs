using TNServicesApp_API.Repositories;
using TNServicesApp_API.Repositories.IRepositories;

namespace TNServicesApp_API.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            return services;
        }
    }
}
