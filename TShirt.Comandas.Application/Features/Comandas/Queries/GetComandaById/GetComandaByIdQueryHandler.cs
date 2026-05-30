using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TShirt.Comandas.Application.Contracts;

namespace TShirt.Comandas.Application.Features.Comandas.Queries.GetComandaById;

public class GetComandaByIdQueryHandler : IRequestHandler<GetComandaByIdQuery, ComandaDto?>
{
    private readonly IComandaRepository _repository;

    public GetComandaByIdQueryHandler(IComandaRepository repository)
    {
        _repository = repository;
    }

    public async Task<ComandaDto?> Handle(GetComandaByIdQuery request, CancellationToken cancellationToken)
    {
        var comanda = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (comanda == null) return null;

        // Mapeo seguro convirtiendo el string de la BD a un JsonDocument real
        var itemsDto = comanda.Items.Select(i => new ItemComandaDto(
            i.Id,
            i.SkuProducto,
            i.DescripcionProducto,
            i.Cantidad,
            string.IsNullOrWhiteSpace(i.NotasConfiguracion)
                ? null
                : JsonDocument.Parse(i.NotasConfiguracion)
        )).ToList();

        return new ComandaDto(
            comanda.Id,
            comanda.NombreCliente,
            comanda.Origen,       // Mapeo real verificado en pgAdmin
            (int)comanda.Estado,  // Mapeo real verificado en pgAdmin
            comanda.FechaCreacion,
            itemsDto
        );
    }
}