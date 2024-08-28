using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.DTO.MappingProfiles
{
    public class EventCategoryMappingProfile : Profile
    {
        public EventCategoryMappingProfile() 
        {
            CreateMap<EventCategoryRequestDto, EventCategory>();
            CreateMap<EventCategory, EventCategoryResponseDto>();
        }
    }
}
