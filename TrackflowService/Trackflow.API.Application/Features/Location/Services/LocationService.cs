using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Application.Features.Location.Interfaces;
using Trackflow.API.Application.Mapping.Loc.Interfaces;
using Trackflow.API.Core.Interfaces;

namespace Trackflow.API.Application.Features.Location.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationMapper _locationMapper;

        public LocationService(ILocationRepository locationRepository,ILocationMapper locationMapper)
        {
            _locationRepository = locationRepository;
            _locationMapper = locationMapper;
        }

        public async Task<LocationToDisplayDto?> GetLatestLocationAsync(int deviceId)
        {
            var latestLocation = await _locationRepository.GetLatestLocationAsync(deviceId);
            if (latestLocation == null) return null;

            return _locationMapper.ToDisplay(latestLocation);
        }

        public async Task<List<LocationToDisplayDto>> GetLocationHistoryAsync(int deviceId)
        {
            var history = await _locationRepository.GetLocationHistoryAsync(deviceId);
            var displayDtos = _locationMapper.ToDisplay(history);

            return displayDtos ?? new List<LocationToDisplayDto>();
        }
    }
}
