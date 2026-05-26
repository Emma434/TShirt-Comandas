using System.Net;
using System.Text.Json;
using FluentValidation;

namespace TShirt.Comandas.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
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
            // AQUÍ ATRAPAMOS LA EXCEPCIÓN DEL BEHAVIOR
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            // Mapeamos los errores de FluentValidation a un formato que el front entienda
            var errors = ex.Errors.Select(e => new {
                Campo = e.PropertyName,
                Mensaje = e.ErrorMessage
            });

            var json = JsonSerializer.Serialize(new
            {
                Mensaje = "Error de validación",
                Detalles = errors
            });

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            // Aquí atrapamos cualquier otro error inesperado (500)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("Error inesperado en el servidor.");
        }
    }
}