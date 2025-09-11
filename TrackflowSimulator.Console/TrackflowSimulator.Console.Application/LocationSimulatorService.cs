using Microsoft.Extensions.Hosting;
using TrackflowSimulator.Console.Core.Interfaces;

namespace TrackflowSimulator.Console.Application
{
    public class LocationSimulatorService : BackgroundService
    {
        private readonly IMessagePublisher _publisher;
        private readonly ILocationGenerator _generator;

        public LocationSimulatorService(IMessagePublisher publisher, ILocationGenerator generator)
        {
            _publisher = publisher;
            _generator = generator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var deviceId = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                var location = _generator.Generate(deviceId);
                await _publisher.PublishAsync(location);

                await Task.Delay(2000, stoppingToken); 
            }
        }
    }
}
