using Microsoft.EntityFrameworkCore;
using Trackflow.API.Core.Entities;
using Trackflow.API.Core.Interfaces;
using Trackflow.API.Infrastructure.Persistance;

namespace Trackflow.API.Infrastructure.Repositories.SqlServerImplementations
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Location?> GetLatestLocationAsync(int deviceId)
        {
            return await _dbContext.Locations
                .Where(l => l.DeviceId == deviceId)
                .OrderByDescending(l => l.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Location>> GetLocationHistoryAsync(int deviceId)
        {
            return await _dbContext.Locations
                .Where(l => l.DeviceId == deviceId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

    }
}
