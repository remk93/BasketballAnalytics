using BA.Core.Queries.Intefaces;
using System.Linq.Expressions;

namespace BA.Core.Queries;

public class BaseSortQuery<TEntity> : BaseQuery<TEntity>, IBaseSortQuery<TEntity> where TEntity : class
{
    public string SortBy { get; set; }
    public bool IsAscending { get; set; }

    protected BaseSortQuery()
    {
        SortBy = "Id";
        IsAscending = false;
    }

    public Expression<Func<TEntity, object>> GetSortingExpression()
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = Expression.Property(parameter, SortBy);

        var expression = Expression.Lambda<Func<TEntity, object>>(property, parameter);
        return expression;
    }
}