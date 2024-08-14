using Events.Domain.Exceptions;
using System.Net;

namespace Events.Application.Exceptions
{
    public class ObjectNotFoundException : BaseException
    {
        public ObjectNotFoundException(
              string details,
              string message = $"Requested object not found",
              HttpStatusCode statusCode = HttpStatusCode.NotFound)
            : base(message, details, statusCode) { }
    }
}
