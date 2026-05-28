using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Domain.Entities;
using MediatR;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

public class CreateComandaCommandHandler : IRequestHandler<CreateComandaCommand, Guid>
{
    public CreateComandaCommandHandler()
    {
    }

    public async Task<Guid> Handle(CreateComandaCommand request, CancellationToken cancellationToken)
    {
        // 1. Instanciamos la Comanda con el nuevo constructor SaaS
        var comanda = new Comanda(request.NombreCliente, request.Origen);

        // 2. Agregamos los ítems dinámicos
        foreach (var item in request.Items)
        {
            comanda.AgregarItem(
                item.SkuProducto,
                item.DescripcionProducto,
                item.Cantidad,
                item.NotasConfiguracion
            );
        }

        // 3. (Mañana conectaremos la base de datos aquí)

        return comanda.Id;
    }
}