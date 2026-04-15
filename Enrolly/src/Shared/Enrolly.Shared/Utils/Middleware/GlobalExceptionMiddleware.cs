using Enrolly.Shared.Logging.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Enrolly.Shared.Logging.Utils.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);
        
        var (statusCode, message) = exception switch
        {
            NotFoundException =>  (StatusCodes.Status404NotFound, exception.Message),
            
            Exception => (StatusCodes.Status500InternalServerError, exception.Message),
            
            _ => (StatusCodes.Status500InternalServerError, exception.Message),
        };
        
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new {error = message});
    }
}