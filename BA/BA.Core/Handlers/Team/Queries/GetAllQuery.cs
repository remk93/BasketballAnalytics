using BA.Core.Queries;
using BA.Core.Queries.Filter;
using System.Linq.Expressions;

namespace BA.Core.Handlers.Team.Queries;

public class GetAllQuery : BaseSortQuery<Domain.Entities.Team>
{
    public FilterModel FilterData { get; set; } = default!;

    public override Expression<Func<Domain.Entities.Team, bool>> GetExpression()
    {
        var filter = base.GetExpression();

        if (FilterData != null)
        {
            if (!string.IsNullOrWhiteSpace(FilterData.SearchByText))
            {
                filter = filter.And(x => x.Name.Contains(FilterData.SearchByText)
                    || x.Code.Contains(FilterData.SearchByText) 
                    || x.City.Contains(FilterData.SearchByText)
                    || x.Stadium.Contains(FilterData.SearchByText));
            }
        }

        return filter;
    }

    public override List<Expression<Func<Domain.Entities.Team, object>>> GetIncludes()
    {
        var includes = base.GetIncludes();

        includes.Add(x => x.Logo);

        return includes;
    }
}