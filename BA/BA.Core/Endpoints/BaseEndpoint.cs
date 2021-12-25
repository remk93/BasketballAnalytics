using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BA.Core.Endpoints;

[Route("[controller]")]
[ApiController]
public class BaseEndpoint : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
}