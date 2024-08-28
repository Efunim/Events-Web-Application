using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IValidateLocationUseCase
    {
        public Task ExecuteAsync(LocationRequestDto locationDto, CancellationToken cancellationToken);
    }
}
