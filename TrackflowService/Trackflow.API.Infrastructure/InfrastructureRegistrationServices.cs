using Microsoft.Extensions.DependencyInjection;
using Trackflow.API.Core.Interfaces;
using Trackflow.API.Infrastructure.Repositories.SqlServerImplementations;

namespace Trackflow.API.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ILocationRepository, LocationRepository>();
            return services;
        }
    }
}
