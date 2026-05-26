using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

public class CreateComandaCommandHandler : IRequestHandler<CreateComandaCommand, Guid>
{
    // Por ahora omitimos la inyección de la base de datos (DbContext/Repository) para enfocarnos en la lógica
    public CreateComandaCommandHandler()
    {
    }

    public async Task<Guid> Handle(CreateComandaCommand request, CancellationToken cancellationToken)
    {
        // 1. Instanciamos la Entidad Principal usando el constructor seguro que creamos ayer
        var comanda = new Comanda(
            request.NombreCliente,
            request.Vendedor,
            request.Talla,
            (TipoPrenda)request.TipoPrendaId, // Convertimos el int (1 o 2) al Enum de Dominio
            request.ColorPrendaId
        );

        // 2. Agregamos los estampados uno por uno, usando el único método permitido
        foreach (var estampado in request.Estampados)
        {
            comanda.AgregarEstampado(
                estampado.SkuDiseno,
                estampado.Ubicacion,
                estampado.TipoEstampado,
                estampado.Cantidad
            );
        }

        // 3. Aquí iría el guardado en base de datos: await _dbContext.Comandas.AddAsync(comanda);
        // await _dbContext.SaveChangesAsync();

        // 4. Retornamos el ID generado
        return comanda.Id;
    }
}