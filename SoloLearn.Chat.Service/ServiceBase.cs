using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SoloLearn.Chat.Service
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository) : base()
        {
            _repository = repository;
        }

        public virtual void Add(TEntity entity)
        {
            _repository.Create(entity);
        }

        public virtual void AddRange(ICollection<TEntity> entities)
        {
            _repository.Create(entities);
        }

        public virtual long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Count(predicate).Result;
        }

        public virtual long Count()
        {
            return _repository.Count().Result;
        }

        public virtual ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.SelectAll(predicate).ToList();
        }

        public virtual ICollection<TEntity> GetAll()
        {
            return _repository.SelectAll().ToList();
        }

        public virtual TEntity GetSingleFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.First(predicate).Result;
        }

        public virtual TEntity GetSingleLast(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Last(predicate).Result;
        }

        public virtual void Remove(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}

