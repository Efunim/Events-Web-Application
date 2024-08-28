using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.DTO.MappingProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile() 
        {
            CreateMap<EventRequestDto, Event>();
            CreateMap<Event, EventResponseDto>();
        }
    }
}
