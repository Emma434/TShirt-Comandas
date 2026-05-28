using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

// Ahora recibimos "Origen" en lugar de "Vendedor", y una lista de "Items" genéricos
public record CreateComandaCommand(
    string NombreCliente,
    string Origen,
    List<ItemComandaDto> Items
) : IRequest<Guid>;

// DTO genérico que sirve para una polera, un café o un repuesto
public record ItemComandaDto(
    string SkuProducto,
    string DescripcionProducto,
    int Cantidad,
    string? NotasConfiguracion
);