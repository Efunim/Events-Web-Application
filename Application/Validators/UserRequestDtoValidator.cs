using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(u => u.Name)
                .NotNull()
                    .WithMessage("Name cannot be null")
                .NotEmpty()
                    .WithMessage("Name is required")
                .Length(2,50)
                    .WithMessage("Name is supposed to be from 2 to 50 letters")
                .Must(BeAValidName)
                    .WithMessage("Name must contain only letters");

            RuleFor(u => u.Surname)
                .NotNull()
                    .WithMessage("Surname cannot be null")
                .NotEmpty()
                    .WithMessage("Surname cannot be empty")
                .Must(BeAValidName)
                    .WithMessage("Surname must contain only letters");

            RuleFor(u => u.Birthday)
                .NotNull()
                    .WithMessage("Birthday cannot be null")
                .NotEmpty()
                    .WithMessage("Birthday is required")
                .Must(BeAValidAge)
                    .WithMessage("Incorrect age");

            RuleFor(u => u.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Invalid email");
        }

        protected bool BeAValidName(string str)
        {
            return str.All(char.IsLetter);
        }

        protected bool BeAValidAge(DateTime? birthday)
        {
            if (!birthday.HasValue)
            {
                return false;
            }

            var currentDate = DateTime.Today;
            var age = currentDate.Year - birthday.Value.Year;

            return age >= 12 && age <= 100;
        }
    }
}
