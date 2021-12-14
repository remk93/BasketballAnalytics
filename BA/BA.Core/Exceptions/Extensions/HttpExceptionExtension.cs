using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BA.Core.Exceptions.Extensions;

public static class HttpExceptionExtension
{
    public static IResult ExceptionToHttpResponse(this Exception exception)
    {
        if (exception is BadRequestException)
        {
            var details = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = exception.Message,
                Instance = exception.Source,
                Detail = exception.StackTrace
            };

            return Results.Problem(details);
        }
        else if (exception is NotFoundException)
        {
            var details = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = exception.Message,
                Instance = exception.Source,
                Detail = exception.StackTrace
            };

            return Results.Problem(details);
        }
        else if (exception is ValidationException)
        {
            var details = new ValidationProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validtion Issue",
                Instance = exception.Source,
                Detail = exception.Message
            };

            return Results.Problem(details);
        }
        else
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = exception.Message,
                Instance = exception.Source,
                Detail = exception.StackTrace
            };

            return Results.Problem(details);
        }
    }
}