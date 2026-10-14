using Ebay.Api.Controllers;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;

namespace Ebay.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

            var (statusCode, apiResponse) = HandleErrorResponse(ex);

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(apiResponse);
        }
    }

    private static (int, ApiResponse) HandleErrorResponse(Exception exception) =>
        exception switch
        {
            ValidationException ex => (StatusCodes.Status400BadRequest,  HandleValidationError(ex.Errors)),
            BadRequestException ex => (StatusCodes.Status400BadRequest, new ApiResponse(new Error("bad.request", ex.Message))),
            NotFoundException ex => (StatusCodes.Status404NotFound, new ApiResponse(new Error("not.found", ex.Message))),
            _ => (StatusCodes.Status500InternalServerError, new ApiResponse(new Error("server.error", exception.Message)))
        };

    private static ApiResponse HandleValidationError(IEnumerable<ValidationError> validationErrors)
    {
        Error error = new Error("validation.error", "One or more validation errors");

        if (!validationErrors.Any())
            return new ApiResponse(error);

        List<string> valErrors = [];

        foreach(var validationError in validationErrors)
        {
            valErrors.Add(validationError.ErrorMessage);
        }

        return new ApiResponse(error, valErrors);
    }
}
