using Events.Application.Interfaces.UseCases;
using Events.Application.Services;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class DeleteEventCategoryUseCase : DeleteEntityUseCase<EventCategory>, IDeleteEventCategoryUseCase
    {
        public DeleteEventCategoryUseCase(IEventCategoryRepository repository) : base(repository) { }
    }
}
