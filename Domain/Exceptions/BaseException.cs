using System.Net;

namespace Events.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? Details { get; }

        public BaseException(string message
            , HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
            : base(message) 
        {
            StatusCode = statusCode;
        }

        public BaseException(string message
            , string details
            , HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode; 
            Details = details;
        }
    }
}
