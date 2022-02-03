using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Commands.Team;
using BA.Core.Exceptions;
using BA.Core.Handlers.Team.Queries;
using BA.Core.Models;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.Team;

public class UpdateHandler : IRequestHandler<UpdateCommand, TeamModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<TeamModel> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        var entity = context.Teams
            .ByQuery(_mapper.Map<GetQuery>(command))
            .FirstOrDefault() ??
                throw new NotFoundException($"Team/{command.Id} was not found");

        await context.Set<Domain.Entities.Team>()
           .Persist(_mapper)
           .InsertOrUpdateAsync(_mapper.Map<TeamModel>(command), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(_mapper.Map<GetCommand>(entity), cancellationToken);
    }
}