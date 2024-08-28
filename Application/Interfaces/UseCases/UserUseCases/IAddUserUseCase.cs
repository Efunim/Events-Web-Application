using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IAddUserUseCase
    {
        public Task<int> ExecuteAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    }
}
