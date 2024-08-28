using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{ 
    public interface IAddEventUseCase
    {
        public Task<int> ExecuteAsync(EventRequestDto eventDto, CancellationToken cancellationToken);
    }
}
