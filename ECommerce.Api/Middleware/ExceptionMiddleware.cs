using Ecommerce.Business.Exceptions;
using System.Text.Json;

namespace Api.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, "AppException: {ErrorCode}", ex.ErrorCode);
            await HandleExceptionAsync(context, ex.StatusCode, ex.ErrorCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(
                context,
                500,
                "internal_error",
                "An unexpected error occurred. Please try again later."
            );
        }
    }

    private static Task HandleExceptionAsync(
           HttpContext context,
           int statusCode,
           string errorCode,
           string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            errorCode,
            message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
