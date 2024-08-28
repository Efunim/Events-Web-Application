using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetLocationUseCase
    {
        public Task<LocationResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
