using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IUpdateUserUseCase
    {
        public Task ExecuteAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken);
    }
}
