using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.DTO.MappingProfiles
{
    public class ParticipantMappingProfile : Profile
    {
        public ParticipantMappingProfile()
        {
            CreateMap<ParticipantRequestDto, Participant>();
            CreateMap<Participant, ParticipantResponseDto>();
        }
    }
}
