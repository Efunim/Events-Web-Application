using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class LocationRequestDtoValidator : AbstractValidator<LocationRequestDto>
    {
        public LocationRequestDtoValidator()
        {
            RuleFor(l => l.House)
                .NotEmpty()
                    .WithMessage("House number required");
        }
    }
}
