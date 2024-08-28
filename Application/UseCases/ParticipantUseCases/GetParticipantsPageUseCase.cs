using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class GetParticipantsPageUseCase : GetEntityPageUseCase<Participant, ParticipantResponseDto>, IGetParticipantsPageUseCase
    {
        public GetParticipantsPageUseCase(IParticipantRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
