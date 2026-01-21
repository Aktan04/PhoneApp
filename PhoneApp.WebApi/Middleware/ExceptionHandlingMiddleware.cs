using System.Net;
using System.Text.Json;
using PhoneApp.Domain.Exceptions;

namespace PhoneApp.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await WriteJson(context, new
            {
                type = "validation_error",
                errors = ex.Errors
            });
        }
        catch (UnauthorizedException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await WriteJson(context, new
            {
                type = "unauthorized",
                message = ex.Message
            });
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await WriteJson(context, new
            {
                type = "not_found",
                message = ex.Message
            });
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await WriteJson(context, new
            {
                type = "server_error",
                message = "Внутренняя ошибка сервера"
            });
        }
    }

    private static async Task WriteJson(HttpContext context, object response)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    }
}