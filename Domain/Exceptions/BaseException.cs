using System.Net;

namespace Events.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public string? Details { get; }

        public BaseException(string message
            , HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
            : base(message) 
        {
        }

        public BaseException(string message
            , string details
            , HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            Details = details;
        }
    }
}
