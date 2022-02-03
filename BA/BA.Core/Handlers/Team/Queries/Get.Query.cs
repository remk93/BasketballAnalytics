using BA.Core.Queries;
using System.Linq.Expressions;

namespace BA.Core.Handlers.Team.Queries;

public class GetQuery : BaseQuery<Domain.Entities.Team>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.Team, bool>> GetExpression()
    {
        return filter => filter.Id == Id;
    }

    public override List<Expression<Func<Domain.Entities.Team, object>>> GetIncludes()
    {
        var includes = base.GetIncludes();

        includes.Add(x => x.Logo);

        return includes; ;
    }
}