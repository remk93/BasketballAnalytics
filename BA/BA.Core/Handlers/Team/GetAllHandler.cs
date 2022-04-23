using AutoMapper;
using AutoMapper.QueryableExtensions;
using BA.Core.Handlers.Team.Commands;
using BA.Core.Handlers.Team.Queries;
using BA.Core.Models;
using BA.Core.Queries;
using BA.Core.Queries.Filter;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.Team;

public class GetAllHandler : IRequestHandler<GetAllCommand, FilteredResult<TeamModel>>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetAllHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<FilteredResult<TeamModel>> Handle(GetAllCommand command, CancellationToken cancellationToken)
    {
        using var context =  await _contextFactory.CreateDbContextAsync(cancellationToken);

        return context.Teams
            .ByQuery(_mapper.Map<GetAllQuery>(command))
            .ProjectTo<TeamModel>(_mapper.ConfigurationProvider)
            .Paginate(command.FilterData);
    }
}