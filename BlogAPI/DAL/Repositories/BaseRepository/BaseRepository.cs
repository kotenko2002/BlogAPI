using BlogAPI.DAL.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Repositories.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> Source;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            Source = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Source.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Source.AddRangeAsync(entities);
        }

        public Task RemoveAsync(TEntity entity)
        {
            Source.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Source.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await Source.ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Source.FindAsync(id);
        }
    }
}
