using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Exceptions;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Commands.Team;

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

        var entity = context.Teams
           .ByQuery(_mapper.Map<GetQuery>(command))
           .FirstOrDefault() ??
               throw new NotFoundException($"Team/{command.Id} was not found");

        context.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}