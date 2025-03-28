using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.Services
{
    public interface ILocationService
    {
        Task<LocationResponseDto> GetLocationAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<LocationResponseDto>> GetAllLocationsAsync(CancellationToken cancellationToken);
        Task<int> InsertLocationAsync(LocationRequestDto locationDto, CancellationToken cancellationToken);
        Task UpdateLocationAsync(int id, LocationRequestDto locationDto, CancellationToken cancellationToken);
        Task DeleteLocationAsync(int id, CancellationToken cancellationToken);
    }
}
