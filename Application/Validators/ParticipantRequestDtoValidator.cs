using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class ParticipantRequestDtoValidator : AbstractValidator<ParticipantRequestDto>
    {
        public ParticipantRequestDtoValidator() 
        {

        }
    }
}
