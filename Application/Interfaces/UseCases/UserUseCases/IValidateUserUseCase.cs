using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IValidateUserUseCase
    {
        public Task ExecuteAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    }
}
