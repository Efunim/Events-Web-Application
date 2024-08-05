using Events.Application.DTO.RequestDTO;
using FluentValidation;

namespace Events.Application.Validators
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator() 
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .Length(2,50)
                    .WithMessage("Name is supposed to be from 2 to 50 letters")
                .Must(BeAValidName)
                    .WithMessage("Name must contain only letters");

            RuleFor(u => u.Surname)
                .NotEmpty()
                    .WithMessage("Surname is required")
                .Must(BeAValidName)
                    .WithMessage("Surname must contain only letters");

            RuleFor(u => u.Birthday)
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

        protected bool BeAValidAge(DateTime birthday)
        {
            var currentDate = DateTime.Today;
            var age = currentDate.Year - birthday.Year;

            return age >= 12 && age <= 100;
        }
    }
}
