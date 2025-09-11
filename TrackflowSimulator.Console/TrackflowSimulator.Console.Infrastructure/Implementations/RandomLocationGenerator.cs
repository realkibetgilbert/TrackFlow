using Trackflow.Contracts;
using TrackflowSimulator.Console.Core.Interfaces;

namespace TrackflowSimulator.Console.Infrastructure.Implementations
{
    public class RandomLocationGenerator : ILocationGenerator
    {
        private readonly Random _random = new Random();
        private readonly double _baseLat = -1.2921;
        private readonly double _baseLng = 36.8219;

        public LocationMessage Generate(int deviceId)
        {
            return new LocationMessage
            {
                DeviceId = deviceId,
                Latitude = _baseLat + (_random.NextDouble() - 0.5) / 100,
                Longitude = _baseLng + (_random.NextDouble() - 0.5) / 100,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
