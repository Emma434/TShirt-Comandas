using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TShirt.Comandas.Application.Features.Comandas.Queries.GetComandaById;

public record GetComandaByIdQuery(Guid Id) : IRequest<ComandaDto?>;