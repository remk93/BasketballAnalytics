using BA.Core.Endpoints;
using BA.Core.Handlers.Team.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BA.Admin.Controllers;

public class TeamController : BaseEndpoint
{
    public TeamController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }


    [HttpPost("GetAll")]
    public async Task<ActionResult> GetAll([FromBody] GetAllCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Get")]
    public async Task<ActionResult> Get([FromBody] GetCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete([FromBody] DeleteCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}