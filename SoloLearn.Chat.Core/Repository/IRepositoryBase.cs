using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn.Chat.Core.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> First(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Last(Expression<Func<TEntity, bool>> predicate);

        Task Create(TEntity entity);

        Task Create(ICollection<TEntity> entities);

        Task Delete(TEntity entity);

        Task Update(TEntity entity);

        IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> SelectAll();
        
        Task<long> Count(Expression<Func<TEntity, bool>> predicate);

        Task<long> Count();
    }
}
