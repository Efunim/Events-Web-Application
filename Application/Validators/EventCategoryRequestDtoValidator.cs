using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class EventCategoryRequestDtoValidator : AbstractValidator<EventCategoryRequestDto>
    {
        public EventCategoryRequestDtoValidator()
        {
            RuleFor(ec => ec.Name)
                .NotEmpty()
                    .WithMessage("Event category name required")
                .Must(BeAValidName)
                    .WithMessage("The category name must contain only letters");
        }

        protected bool BeAValidName(string str)
        {
            return str.All(char.IsLetter);
        }
    }
}
