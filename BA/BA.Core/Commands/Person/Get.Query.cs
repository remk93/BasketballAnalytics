using BA.Core.Queries;
using System.Linq.Expressions;

namespace BA.Core.Commands.Person;

public class GetQuery : BaseQuery<Domain.Entities.Person>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.Person, bool>> GetExpression()
    {
        return filter => filter.Id == Id;
    }
}