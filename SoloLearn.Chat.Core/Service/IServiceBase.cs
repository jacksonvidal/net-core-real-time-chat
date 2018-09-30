using SoloLearn.Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SoloLearn.Chat.Core.Service
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        TEntity GetSingleFirst(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleLast(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Add(ICollection<TEntity> entities);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        ICollection<TEntity> GetAll();

        long Count(Expression<Func<TEntity, bool>> predicate);
        long Count();
    }
}
