using Trackflow.API.Application.DTOs.Location;

namespace Trackflow.API.Application.Features.Location.Interfaces
{
    public interface ILocationService
    {
        Task<LocationToDisplayDto?> GetLatestLocationAsync(int deviceId);

        Task<List<LocationToDisplayDto>> GetLocationHistoryAsync(int deviceId);
    }
}
