using Events.Domain.Entities;

namespace Events.Application.Interfaces.UseCases
{
    public interface IDeleteEventCategoryUseCase
    {
        public Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
