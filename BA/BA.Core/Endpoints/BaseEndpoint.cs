using BA.Core.Exceptions.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BA.Core.Endpoints;

public class BaseEndpoint
{
    protected readonly IMediator _mediator;

    public BaseEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IResult> HandleAsync<TResponse, TCommand>(TCommand command, CancellationToken cancellationToken = new CancellationToken()) where TCommand : IRequest<TResponse>
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            return result is Unit
                ? Results.NoContent()
                : Results.Ok(result);
        }
        catch (Exception ex)
        {
            return ex.ExceptionToHttpResponse();
        }
    }
}