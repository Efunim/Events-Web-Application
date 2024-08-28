using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IUpdateLocationUseCase
    {
        public Task ExecuteAsync(int id, LocationRequestDto locationDto, CancellationToken cancellationToken);
    }
}
