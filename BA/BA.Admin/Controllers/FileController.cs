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

    [HttpPost("Upload")]
    public async Task<ActionResult> Upload(UploadCommand command)
    {
        var result = await _mediator.Send(command);
        return File(result, ContentType(command.Link), command.Name);
    }

    private string ContentType(string fileName)
    {
        var contentType = "application/octet-stream";
        var extenstion = Path.GetExtension(fileName)?.ToLower();
        switch (extenstion)
        {
            case ".png":
                contentType = "image/png";
                break;
            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;
        }
        return contentType;
    }
}