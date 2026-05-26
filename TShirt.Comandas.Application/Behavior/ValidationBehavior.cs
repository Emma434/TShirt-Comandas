using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace TShirt.Comandas.Application.Behaviors;

// Esta clase intercepta TODOS los comandos antes de que lleguen a sus Handlers
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    // Inyectamos todos los validadores que existan para este comando específico
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // Ejecuta todas las reglas de validación en paralelo
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Recolecta todos los errores encontrados
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                // ¡EL GUARDIA ACTÚA! Lanza una excepción con los mensajes amigables.
                // El flujo se corta aquí. El Handler (Cocinero) NUNCA se entera de que esto pasó.
                throw new ValidationException(failures);
            }
        }

        // Si no hay errores, el guardia abre la puerta y llama al Handler
        return await next();
    }
}