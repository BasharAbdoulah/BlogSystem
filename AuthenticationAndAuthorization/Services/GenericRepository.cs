using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogSystem.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected BlogDataContext _context;
        internal DbSet<T> _dbSet { get; set; }
        protected readonly ILogger _logger;


        public GenericRepository(BlogDataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this._dbSet = _context.Set<T>();
        }
        public virtual async Task<bool> Add(T entity)
        {
           await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();  
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }


        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
