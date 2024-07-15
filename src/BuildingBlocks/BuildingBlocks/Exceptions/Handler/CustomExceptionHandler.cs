using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Time of Occurrence: {time}", exception.Message, DateTime.UtcNow);

        (string Title, int StatusCode, string Details) detail = exception switch
        {
            NotFoundException =>
            (
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound,
                exception.Message
            ),
            BadRequestException =>
            (
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest,
                exception.Message
            ),
            ValidationException =>
            (
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest,
                exception.Message
            ),
            InternalServerException =>
            (
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError,
                exception.Message
            ),
            _ =>
            ("InternalServerException", StatusCodes.Status500InternalServerError, exception.Message)
        };

        var problem = new ProblemDetails
        {
            Title = detail.Title,
            Status = detail.StatusCode,
            Detail = detail.Details,
            Instance = httpContext.Request.Path
        };
        problem.Extensions.Add("traceId", httpContext.TraceIdentifier);

        // if it was a FluentValidation Validation exception, add the validation errors
        if (exception is ValidationException validationException)
            problem.Extensions.Add("validationErrors", validationException.Errors);

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}
