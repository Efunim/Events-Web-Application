using Events.Domain.Exceptions;
using System.Net;

namespace Events.Application.Exceptions
{
    public class NonUniqueException : BaseException
    {
        public NonUniqueException(
            string message = "Value is not unique")
            : base(message) { }
    }
}
