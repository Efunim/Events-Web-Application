using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Filters;

namespace Events.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<EventResponseDto> GetEventAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EventResponseDto>> GetEventsByCriteriaAsync(EventFilter eventFilter, CancellationToken cancellationToken);
        Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(CancellationToken cancellationToken);
        Task<int> InsertEventAsync(EventRequestDto eventDto, CancellationToken cancellationToken);
        Task UpdateEventAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken);
        Task DeleteEventAsync(int id, CancellationToken cancellationToken);
    }
}
