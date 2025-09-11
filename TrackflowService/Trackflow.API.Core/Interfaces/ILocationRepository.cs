using Trackflow.API.Core.Entities;

namespace Trackflow.API.Core.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location?> GetLatestLocationAsync(int deviceId);
        Task<IEnumerable<Location>> GetLocationHistoryAsync(int deviceId);
    }
}
