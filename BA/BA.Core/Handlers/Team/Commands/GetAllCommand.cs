using BA.Core.Models;
using BA.Core.Queries.Filter;
using MediatR;

namespace BA.Core.Handlers.Team.Commands;

public class GetAllCommand : IRequest<FilteredResult<TeamModel>>
{
    public FilterModel FilterData { get; set; } = default!;
}