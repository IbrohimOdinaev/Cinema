using Cinema.Domain.Exceptions;

namespace Cinema.Api;

public class ExceptionHandlerMiddleware
{
    private RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainArgumentException ex)
        {
            await HandleExceptionAsync(context, ex, 400, ex.Title);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, 500, "Internal Server Error");
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string title)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            TimeStamp = DateTime.UtcNow
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}

