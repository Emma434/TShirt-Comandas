using MediatR;
using Microsoft.AspNetCore.Mvc;
using TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateComandaCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
