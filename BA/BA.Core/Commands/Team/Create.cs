using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Models;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Commands.Team;

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
        using var context = _contextFactory.CreateDbContext();

        var entity = await context.Set<Domain.Entities.Team>()
            .Persist(_mapper)
            .InsertOrUpdateAsync(command, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(_mapper.Map<GetCommand>(entity), cancellationToken);
    }
}