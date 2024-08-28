using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IAddEventCategoryUseCase
    {
        public Task<int> ExecuteAsync(EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken);
    }
}
