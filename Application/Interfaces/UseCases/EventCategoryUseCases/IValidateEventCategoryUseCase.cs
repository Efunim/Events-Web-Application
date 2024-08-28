using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IValidateEventCategoryUseCase
    {
        public Task ExecuteAsync(EventCategoryRequestDto eventCategory, CancellationToken cancellationToken);
    }
}
