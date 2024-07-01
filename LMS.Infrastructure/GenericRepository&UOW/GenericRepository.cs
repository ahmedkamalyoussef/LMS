using LMS.Data.Consts;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Infrastructure.GenericRepository_UOW
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy = null, string direction = null, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(expression);

            if (orderBy != null)
            {
                if (direction == OrderDirection.Descending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> FilterAsync(int pageSize, int pageIndex,List< Expression<Func<T, bool>>> expressions,  Expression<Func<T, object>> orderBy = null, string direction = null, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var expression in expressions)
            {
                query=query.Where(expression);
            }

            if (orderBy != null)
            {
                if (direction == OrderDirection.Descending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            query = query.Skip((pageIndex - 1) *pageSize).Take( pageSize);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }
        public async Task<T> FindFirstAsync(Expression<Func<T, bool>> expression, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(expression);
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null, string direction = null, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (orderBy != null)
            {
                if (direction == OrderDirection.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
