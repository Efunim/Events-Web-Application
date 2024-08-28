using Events.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Events_Web_application.Infrastructure
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly Dictionary<Type, HttpStatusCode> exceptionToStatusCodeMap =
            new Dictionary<Type, HttpStatusCode>
            {
                { typeof(NonUniqueException), HttpStatusCode.BadRequest },
                { typeof(ArgumentOutOfRangeException), HttpStatusCode.BadRequest },
                { typeof(ObjectNotFoundException), HttpStatusCode.NotFound }
            };

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception
            , CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;
            problemDetails.Detail = exception.Message;

            httpContext.Response.StatusCode = (int)(exceptionToStatusCodeMap.TryGetValue(exception.GetType(), out var statusCode)
                ? statusCode
                : HttpStatusCode.InternalServerError);

            logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
