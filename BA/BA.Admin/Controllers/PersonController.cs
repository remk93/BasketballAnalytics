using BA.Core.Commands.Person;
using BA.Core.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BA.Admin.Controllers;

public class PersonController : BaseEndpoint
{
    public PersonController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateCommand command)
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