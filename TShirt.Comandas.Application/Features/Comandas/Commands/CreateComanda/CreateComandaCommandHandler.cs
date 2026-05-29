using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

public class CreateComandaCommandHandler : IRequestHandler<CreateComandaCommand, Guid>
{
    private readonly IComandaRepository _repository;

    public CreateComandaCommandHandler(IComandaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateComandaCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear el Aggregate Root con el Origen Genérico
        var comanda = new Comanda(request.NombreCliente, request.Origen);

        // 2. Mapear los DTOs dinámicos al Dominio
        foreach (var item in request.Items)
        {
            comanda.AgregarItem(
                item.SkuProducto,
                item.DescripcionProducto,
                item.Cantidad,
                item.NotasConfiguracion
            );
        }

        // 3. Persistencia física en PostgreSQL
        await _repository.AddAsync(comanda, cancellationToken);

        return comanda.Id;
    }
}