using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class EventRequestDtoValidator : AbstractValidator<EventRequestDto>
    {
        public EventRequestDtoValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Event name required");

            RuleFor(e => e.Description)
                .NotEmpty()
                    .WithMessage("Event description required");

            RuleFor(e => e.EventTime)
                .NotEmpty()
                    .WithMessage("Event date and time required")
                .Must(BeValidDate)
                    .WithMessage("Event can be added at least one day before the event");

            RuleFor(e => e.MaxParticipants)
                .GreaterThan(0)
                    .WithMessage("Invalid max participants number");

            RuleFor(e => e.ImagePath)
                .NotEmpty()
                .Must(BeAnExistingImage)
                    .WithMessage("Couldn't find image on server");

        }

        protected bool BeValidDate(DateTime dateTime)  
        {
            return dateTime > (DateTime.Now.AddDays(1));
        }

        protected bool BeAnExistingImage(string imagePath)
        {
            return File.Exists($"{Directory.GetCurrentDirectory()}\\uploads\\{imagePath}");
        }
    }
}
