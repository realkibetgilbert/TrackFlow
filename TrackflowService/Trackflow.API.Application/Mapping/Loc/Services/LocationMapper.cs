using AutoMapper;
using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Application.Mapping.Loc.Interfaces;
using Trackflow.API.Core.Entities;

namespace Trackflow.API.Application.Mapping.Loc.Services
{
    public class LocationMapper : ILocationMapper
    {
        private readonly IMapper _mapper;

        public LocationMapper(IMapper mapper )
        {
            _mapper = mapper;
        }
        public List<LocationToDisplayDto> ToDisplay(IEnumerable<Location> locations)
        {
            return _mapper.Map<List<LocationToDisplayDto>>(locations);
        }

        public LocationToDisplayDto ToDisplay(Location location)
        {
            return _mapper.Map<LocationToDisplayDto>(location);
        }
    }
}
