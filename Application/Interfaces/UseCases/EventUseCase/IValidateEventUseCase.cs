using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IValidateEventUseCase
    {
        public Task ExecuteAsync(EventRequestDto eventDto, CancellationToken cancellationToken);
    }
}
