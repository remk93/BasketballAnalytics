using BA.Core.Queries;
using System.Linq.Expressions;

namespace BA.Core.Commands.Team;

public class GetQuery : BaseQuery<Domain.Entities.Team>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.Team, bool>> GetExpression()
    {
        return filter => filter.Id == Id;
    }
}