using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Core.Data
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ChatDbContext _dbContext;

        public RepositoryBase(ChatDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().CountAsync(predicate);
        }

        public virtual async Task<long> Count()
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().CountAsync();
        }

        public virtual async Task Create(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public virtual async Task Create(ICollection<TEntity> entities)
        {
            await this._dbContext.Set<TEntity>().AddRangeAsync(entities);
            await this._dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Deleted;
            this._dbContext.Set<TEntity>().Remove(entity);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> First(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        

        public async Task<TEntity> Last(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().LastOrDefaultAsync(predicate);
        }

        public virtual IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this._dbContext.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<TEntity> SelectAll()
        {
            return this._dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public async Task Update(TEntity entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;

            this._dbContext.Set<TEntity>().Attach(entity);

            await this._dbContext.SaveChangesAsync();
        }
    }
}
