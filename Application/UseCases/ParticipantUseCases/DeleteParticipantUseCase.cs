using Events.Application.DTO.RequestDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class DeleteParticipantUseCase : DeleteEntityUseCase<Participant>, IDeleteParticipantUseCase
    {
        public DeleteParticipantUseCase(IParticipantRepository repository) : base(repository) { }
    }
}
