using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TShirt.Comandas.Application.Features.Comandas.Queries.GetComandaById;

public record ComandaDto(
    Guid Id,
    string NombreCliente,
    string Origen, 
    int Estado,    
    DateTime FechaCreacion,
    IReadOnlyCollection<ItemComandaDto> Items
);

public record ItemComandaDto(
    Guid Id,
    string SkuProducto,
    string DescripcionProducto,
    int Cantidad,
    JsonDocument? NotasConfiguracion
);