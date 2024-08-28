using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class UpdateParticipantUseCase : UpdateEntityUseCase<Participant, ParticipantRequestDto>, IUpdateParticipantUseCase
    {
        public UpdateParticipantUseCase(IParticipantRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _validationUseCase = new ValidateParticipantUseCase();
        }
    }
}
