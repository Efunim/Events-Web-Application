using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.DTO
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile() 
        {
            CreateMap<EventCategoryRequestDto, EventCategory>();
            CreateMap<EventRequestDto, Event>();
            CreateMap<LocationRequestDto, Location>();
            CreateMap<ParticipantRequestDto, Participant>();
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<EventCategory, EventCategoryResponseDto>();
            CreateMap<Participant, ParticipantResponseDto>();
            CreateMap<User, UserResponseDto>();
            CreateMap<Event, EventResponseDTO>()
                .ForMember(dest => dest.CurrentParticipants, opt => opt.MapFrom(src => src.Participants.Count));

            CreateMap<Location, LocationResponseDto>()
                .ForMember(dest => dest.FullAddress, opt
                => opt.MapFrom(src => $"{src.Street.Name} {src.House}, {src.Street.City.Name}, {src.Street.City.Country.Name}"));
        }
    }
}
