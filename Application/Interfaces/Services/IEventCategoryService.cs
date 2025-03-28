using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.Services
{
    public interface IEventCategoryService
    {
        Task<EventCategoryResponseDto> GetEventCategoryAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EventCategoryResponseDto>> GetAllEventCategoriesAsync(CancellationToken cancellationToken);
        Task<int> InsertEventCategoryAsync(EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken);
        Task UpdateEventCategoryAsync(int id, EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken);
        Task DeleteEventCategoryAsync(int id, CancellationToken cancellationToken);
    }
}
