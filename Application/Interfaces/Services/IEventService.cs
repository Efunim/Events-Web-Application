using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Specifications;
using Events.Application.Specifications.EventSpecifications;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<EventResponseDto> GetEventAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EventResponseDto>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        IEnumerable<EventResponseDto> GetEventsByCriteria(Specification<Event>? specification, int pageIndex, int pageSize);
        Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(CancellationToken cancellationToken);
        Task<int> InsertEventAsync(EventRequestDto eventDto, CancellationToken cancellationToken);
        Task UpdateEventAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken);
        Task DeleteEventAsync(int id, CancellationToken cancellationToken);
    }
}
