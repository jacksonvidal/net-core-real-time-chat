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
    /// <summary>
    /// all the basic abstract data operations
    /// </summary>
    /// <typeparam name="TEntity">Entities</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// datacontext
        /// </summary>
        private readonly ChatDbContext _dbContext;

        /// <summary>
        /// instance of the datacontext
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        public RepositoryBase(ChatDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Count specifc dataset based on a predica lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>long</returns>
        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().CountAsync(predicate);
        }

        /// <summary>
        /// Count stored entities
        /// </summary>
        /// <returns>long</returns>
        public virtual async Task<long> Count()
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().CountAsync();
        }

        /// <summary>
        /// Insert new entity at the database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public virtual async Task Create(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Insert new set of entities at the database
        /// </summary>
        /// <param name="entity">List of Entitiesy</param>
        /// <returns></returns>
        public virtual async Task Create(ICollection<TEntity> entities)
        {
            await this._dbContext.Set<TEntity>().AddRangeAsync(entities);
            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from the database
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        public virtual async Task Delete(TEntity entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Deleted;
            this._dbContext.Set<TEntity>().Remove(entity);

            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrive the first row based on a predicate lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> First(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Retrive the last row based on a predicate lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> Last(Expression<Func<TEntity, bool>> predicate)
        {
            return await this._dbContext.Set<TEntity>().AsNoTracking().LastOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Retrive all rows based on a predicate lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>IQueryable</returns>
        public virtual IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this._dbContext.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable();
        }


        /// <summary>
        /// Retrive all rows of an entity
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> SelectAll()
        {
            return this._dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Update a spefic entity at the database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public async Task Update(TEntity entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;

            this._dbContext.Set<TEntity>().Attach(entity);

            await this._dbContext.SaveChangesAsync();
        }
    }
}
