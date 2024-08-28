using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.DTO.MappingProfiles
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<LocationRequestDto, Location>();
            CreateMap<Location, LocationResponseDto>();
        }
    }
}
