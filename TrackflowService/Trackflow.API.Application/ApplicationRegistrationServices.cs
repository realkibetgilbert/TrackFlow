using Microsoft.Extensions.DependencyInjection;
using Trackflow.API.Application.Features.Location.Interfaces;
using Trackflow.API.Application.Features.Location.Services;
using Trackflow.API.Application.Mapping.Loc;
using Trackflow.API.Application.Mapping.Loc.Interfaces;
using Trackflow.API.Application.Mapping.Loc.Services;

namespace Trackflow.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<ILocationService, LocationService>();
            services.AddTransient<ILocationMapper, LocationMapper>();
            services.AddAutoMapper(typeof(LocationMappingProfile));

            return services;
        }
    }
}
