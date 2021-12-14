using System.Linq.Expressions;

namespace BA.Core.Queries.Intefaces
{
    public interface IBaseQuery<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> GetExpression();
        List<Expression<Func<TEntity, object>>> GetIncludes();
    }
}
