using MassTransit;
using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Application.Interfaces;
using Trackflow.API.Core.Entities;
using Trackflow.API.Infrastructure.Persistance;
using Trackflow.Contracts;

namespace Trackflow.API.Infrastructure.Messaging
{
    public class LocationConsumer : IConsumer<LocationMessage>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILocationNotifier _locationNotifier;

        public LocationConsumer(ApplicationDbContext dbContext, ILocationNotifier locationNotifier)
        {
            _dbContext = dbContext;
            _locationNotifier = locationNotifier;
        }

        public async Task Consume(ConsumeContext<LocationMessage> context)
        {
            var msg = context.Message;

            var entity = new Location
            {
                DeviceId = msg.DeviceId,
                Latitude = msg.Latitude,
                Longitude = msg.Longitude,
                Timestamp = msg.Timestamp
            };

            _dbContext.Locations.Add(entity);
            await _dbContext.SaveChangesAsync(context.CancellationToken);

            var dto = new LocationToDisplayDto
            {
                DeviceId = entity.DeviceId,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Timestamp = entity.Timestamp
            };

            await _locationNotifier.NotifyLocationAsync(dto);
        }
    }
}
