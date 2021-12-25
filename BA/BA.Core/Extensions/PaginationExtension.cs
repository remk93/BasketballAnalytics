using BA.Core.Queries.Filter;

namespace BA.Core.Extensions;

public static class PaginationExtension
{
    public static FilteredResult<TEntity> PageResult<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize)
        where TEntity : class
    {
        var count = query.Count();

        var skip = (pageNumber - 1) * pageSize;
        var result = query.Skip(skip).Take(pageSize).ToList();

        return new FilteredResult<TEntity>(count, result);
    }
}