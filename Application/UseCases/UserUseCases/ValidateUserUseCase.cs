using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Validators;

namespace Events.Application.UseCases
{
    public class ValidateUserUseCase : IValidationUseCase<UserRequestDto>
    {
        public async Task ExecuteAsync(UserRequestDto user, CancellationToken cancellationToken)
        {
            UserRequestDtoValidator validator = new UserRequestDtoValidator();
            var results = await validator.ValidateAsync(user);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
