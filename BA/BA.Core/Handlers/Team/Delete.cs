using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Commands.Team;
using BA.Core.Exceptions;
using BA.Core.Handlers.Team.Queries;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.Team;

public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DeleteHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        var model = await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);

        await context.BeginTransactionAsync();

        await context.Teams
            .Persist(_mapper)
            .RemoveAsync(model, cancellationToken);

        await context.CommitTransactionAsync();

        return Unit.Value;
    }
}