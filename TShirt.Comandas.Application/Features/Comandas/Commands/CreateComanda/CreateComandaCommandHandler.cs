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
    private readonly IComandaRepository _repository;

    // Inyección de dependencias pura. MediatR y el contenedor de .NET se encargarán de pasar la implementación real.
    public CreateComandaCommandHandler(IComandaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateComandaCommand request, CancellationToken cancellationToken)
    {
        // USAMOS LA FACTORY METHOD, no el new Comanda()
        var comanda = Comanda.Crear(request.NombreCliente, request.DescripcionPedido);

        await _repository.AddAsync(comanda);

        return comanda.Id;
    }
}