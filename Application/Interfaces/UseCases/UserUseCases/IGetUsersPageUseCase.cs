using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetUsersPageUseCase
    {
        public Task<IEnumerable<UserResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
