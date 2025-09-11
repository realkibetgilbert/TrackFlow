using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Core.Entities;

namespace Trackflow.API.Application.Mapping.Loc.Interfaces
{
    public interface ILocationMapper
    {
        List<LocationToDisplayDto> ToDisplay(IEnumerable<Location> locations);
        LocationToDisplayDto ToDisplay(Location location);
    }
}
