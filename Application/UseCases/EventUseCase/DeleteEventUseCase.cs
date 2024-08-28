using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class DeleteEventUseCase : DeleteEntityUseCase<Event>, IDeleteEventUseCase
    {
        public DeleteEventUseCase(IEventRepository repository) : base(repository) { }
    }
}
