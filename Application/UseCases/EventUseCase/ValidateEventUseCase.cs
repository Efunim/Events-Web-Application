using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Validators;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class ValidateEventUseCase : IValidationUseCase<EventRequestDto>
    {
        public async Task ExecuteAsync(EventRequestDto @event, CancellationToken cancellationToken)
        {
            EventRequestDtoValidator validator = new EventRequestDtoValidator();
            var results = await validator.ValidateAsync(@event);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
