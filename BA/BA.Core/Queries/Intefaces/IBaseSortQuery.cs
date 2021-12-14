using System.Linq.Expressions;

namespace BA.Core.Queries.Intefaces
{
    public interface IBaseSortQuery<TEntity> : IBaseQuery<TEntity> where TEntity : class
    {
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }

        public Expression<Func<TEntity, object>> GetSortingExpression();
    }
}