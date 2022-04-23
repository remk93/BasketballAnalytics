using AutoMapper;
using AutoMapper.QueryableExtensions;
using BA.Core.Handlers.Team.Commands;
using BA.Core.Exceptions;
using BA.Core.Handlers.Team.Queries;
using BA.Core.Models;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.Team;

public class GetHandler : IRequestHandler<GetCommand, TeamModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<TeamModel> Handle(GetCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        return await context.Teams
            .ByQuery(_mapper.Map<GetQuery>(command))
            .ProjectTo<TeamModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"Team/{command.Id} was not found");
    }
}