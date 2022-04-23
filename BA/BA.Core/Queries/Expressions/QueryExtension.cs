using BA.Core.Queries.Intefaces;
using BA.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using BA.Core.Queries.Filter;

namespace BA.Core.Queries;

public static class QuriableExtension
{
    public static IQueryable<TEntity> ByQuery<TEntity>(this IQueryable<TEntity> items, IBaseQuery<TEntity> query) where TEntity : class
    {
        var result = items.Where(query.GetExpression());
        query.GetIncludes().ForEach(i => result.Include(i));
        return result;
    }

    public static IOrderedQueryable<TEntity> ByQuery<TEntity>(this IQueryable<TEntity> items, IBaseSortQuery<TEntity> query) where TEntity : class
    {
        var result = items.Where(query.GetExpression());
        query.GetIncludes().ForEach(i => result.Include(i));
        return result.OrderBy(query.SortBy, query.IsAscending);
    }

    public static FilteredResult<TEntity> Paginate<TEntity>(this IQueryable<TEntity> items, FilterModel filterModel) where TEntity : class
    {
        return items.PageResult(filterModel.PageNumber, filterModel.PageSize);
    }
}