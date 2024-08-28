using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IUpdateEventUseCase
    {
        public Task ExecuteAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken);
    }
}
