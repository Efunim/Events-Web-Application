using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<int> InsertUserAsync(UserRequestDto UserDto, CancellationToken cancellationToken);
        Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}
