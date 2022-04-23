using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Handlers.Team.Commands;
using BA.Core.Models;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.Team;

public class CreateHandler : IRequestHandler<CreateCommand, TeamModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<TeamModel> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Teams
            .Persist(_mapper)
            .InsertOrUpdateAsync(command, cancellationToken);

        await context.CommitTransactionAsync();

        command.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);
    }
}