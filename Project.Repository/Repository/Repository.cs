using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.Repository.Common.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public Task RemoveAsync(TEntity entity)
        {
            Context.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public Task RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return Context.SaveChangesAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(int id, TEntity entity)
        {
            EntityEntry entityEntry = Context.Entry<TEntity>(entity);
            entityEntry.State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> FindAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }


    }
}

