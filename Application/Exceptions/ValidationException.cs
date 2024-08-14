using FluentValidation.Results;

namespace Events.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string> Errors { get; } 
        public ValidationException(IEnumerable<ValidationFailure> validationFailures) : base("Validation error occured")
        {
            Errors = validationFailures.ToDictionary(failure => failure.PropertyName,
                failure => failure.ErrorMessage);
        }
    }
}
