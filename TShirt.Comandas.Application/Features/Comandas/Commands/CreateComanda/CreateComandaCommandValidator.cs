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
        RuleFor(p => p.NombreCliente)
            .NotEmpty().WithMessage("El nombre del cliente es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre del cliente no puede exceder los 100 caracteres.");

        RuleFor(p => p.Origen)
            .NotEmpty().WithMessage("El origen de la comanda (ej: Sucursal, Mesa, App) es obligatorio.");

        RuleFor(p => p.Items)
            .NotEmpty().WithMessage("La comanda debe tener al menos un ítem.");
    }
}