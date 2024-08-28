using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IAddLocationUseCase
    {
        public Task<int> ExecuteAsync(LocationRequestDto locationDto, CancellationToken cancellationToken);
    }
}
