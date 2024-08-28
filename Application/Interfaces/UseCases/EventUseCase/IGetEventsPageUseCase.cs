using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;
using Events.Domain.Specifications;
using Events.Domain.Specifications.EventSpecifications;

namespace Events.Application.Interfaces.UseCases
{ 
    public interface IGetEventsPageUseCase
    {
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync(ISpecification<Event> filter, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
