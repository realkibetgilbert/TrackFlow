using AutoMapper;
using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Core.Entities;

namespace Trackflow.API.Application.Mapping.Loc
{
    public class LocationMappingProfile: Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationToDisplayDto>().ReverseMap();
        }
    }
}
