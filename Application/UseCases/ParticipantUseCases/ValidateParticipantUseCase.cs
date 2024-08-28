using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Validators;

namespace Events.Application.UseCases
{
    public class ValidateParticipantUseCase : IValidationUseCase<ParticipantRequestDto>
    {
        public async Task ExecuteAsync(ParticipantRequestDto participant, CancellationToken cancellationToken)
        {
            ParticipantRequestDtoValidator validator = new ParticipantRequestDtoValidator();
            var results = await validator.ValidateAsync(participant);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
