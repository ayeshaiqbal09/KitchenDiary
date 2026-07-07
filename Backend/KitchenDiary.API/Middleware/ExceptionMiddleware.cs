using System.Text.Json;

namespace KitchenDiary.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next=next;
        _logger=logger;
        
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred."
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }

}