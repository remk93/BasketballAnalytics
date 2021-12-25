using BA.Core.Commands.File;
using BA.Core.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BA.Admin.Controllers;

public class FileController : BaseEndpoint
{
    public FileController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Download")]
    public async Task<ActionResult> Download([FromForm] DownloadCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}