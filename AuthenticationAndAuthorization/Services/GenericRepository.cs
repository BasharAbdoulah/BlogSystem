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
                var entities = await _dbSet.ToListAsync();  
                    return(entities);
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


        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return (entity);
        }


    }
}
