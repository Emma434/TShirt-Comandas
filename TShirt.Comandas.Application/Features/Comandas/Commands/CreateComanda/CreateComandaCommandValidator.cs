using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

public class CreateComandaCommandValidator : AbstractValidator<CreateComandaCommand>
{
    public CreateComandaCommandValidator()
    {
        // Reglas básicas de formato y coherencia
        RuleFor(p => p.NombreCliente)
            .NotEmpty().WithMessage("El nombre del cliente es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre del cliente no puede exceder los 100 caracteres.");

        RuleFor(p => p.ColorPrendaId)
            .GreaterThan(0).WithMessage("Debe seleccionar un color de prenda válido.");

        RuleFor(p => p.TipoPrendaId)
            .InclusiveBetween(1, 2).WithMessage("El tipo de prenda debe ser 1 (Polera) o 2 (Poleron).");

        // Validamos que al menos envíen un estampado
        RuleFor(p => p.Estampados)
            .NotEmpty().WithMessage("La comanda debe tener al menos un estampado detallado.");

        // NOTA DE ARQUITECTURA: Aquí es donde más adelante inyectaremos la interfaz 
        // de la base de datos para hacer un .MustAsync() y verificar si el ColorId '999' existe.
    }
}
