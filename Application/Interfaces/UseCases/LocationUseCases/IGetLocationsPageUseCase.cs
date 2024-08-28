using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetLocationsPageUseCase
    {
        public Task<IEnumerable<LocationResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
