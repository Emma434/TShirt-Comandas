using MediatR;
using Microsoft.AspNetCore.Mvc;
using TShirt.Comandas.Application.Features.Comandas.Queries.GetComandaById;

namespace TShirt.Comandas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComandasController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComandasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ENDPOINT DE LECTURA ESTRUCTURADA
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetComandaByIdQuery(id);
        var comandaDto = await _mediator.Send(query, cancellationToken);

        if (comandaDto == null)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = $"La comanda con ID {id} no existe en el sistema."
            });
        }

        // El DTO viaja con el JsonDocument nativo; Kestrel lo serializará al cliente de forma perfecta
        return Ok(comandaDto);
    }
}