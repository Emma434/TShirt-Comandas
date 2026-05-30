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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // HTTP 400

            // Mapeamos los errores en un diccionario limpio: Propiedad -> Lista de errores
            var erroresDicionario = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                message = "Fallo en la validación de los datos de entrada.",
                errors = erroresDicionario
            };

            var json = JsonSerializer.Serialize(response);
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