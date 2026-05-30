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
            .NotEmpty().WithMessage("El nombre del cliente no puede estar vacío.")
            .MaximumLength(100).WithMessage("El nombre del cliente no puede superar los 100 caracteres.");

        RuleFor(p => p.Origen)
            .NotEmpty().WithMessage("El origen de la comanda es obligatorio.");

        RuleFor(p => p.Items)
            .NotEmpty().WithMessage("La comanda debe contener al menos un ítem.");

        // Reglas en cascada para la colección de ítems hijos
        RuleForEach(p => p.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.SkuProducto)
                .NotEmpty().WithMessage("El SKU del producto es requerido.");

            item.RuleFor(i => i.DescripcionProducto)
                .NotEmpty().WithMessage("La descripción del producto es requerida.");

            item.RuleFor(i => i.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
        });
    }
}