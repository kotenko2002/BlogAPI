﻿using BlogAPI.DAL.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Repositories.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> Sourse;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            Sourse = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Sourse.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Sourse.AddRangeAsync(entities);
        }

        public Task RemoveAsync(TEntity entity)
        {
            Sourse.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Sourse.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await Sourse.ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Sourse.FindAsync(id);
        }
    }
}
