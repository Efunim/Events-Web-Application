using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Validators;

namespace Events.Application.UseCases
{
    public class ValidateLocationUseCase : IValidationUseCase<LocationRequestDto>
    {
        public async Task ExecuteAsync(LocationRequestDto location, CancellationToken cancellationToken)
        {
            LocationRequestDtoValidator validator = new LocationRequestDtoValidator();
            var results = await validator.ValidateAsync(location);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
