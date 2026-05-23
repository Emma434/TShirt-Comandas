using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

public record CreateComandaCommand(
    string NombreCliente,
    string DescripcionPedido
) : IRequest<Guid>;
