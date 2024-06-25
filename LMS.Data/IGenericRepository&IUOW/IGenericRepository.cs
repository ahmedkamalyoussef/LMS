using System.Linq.Expressions;

namespace LMS.Data.IGenericRepository_IUOW
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null, string direction = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<int> CountAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy = null, string direction = null);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> expression);
    }
}
