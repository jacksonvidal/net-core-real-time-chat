using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Repository;
using SoloLearn.Chat.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SoloLearn.Chat.Service
{
    /// <summary>
    /// all the basic abstract business operations
    /// </summary>
    /// <typeparam name="TEntity">Entities</typeparam>
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// injection of the repository
        /// </summary>
        private readonly IRepositoryBase<TEntity> _repository;

        /// <summary>
        /// constructor to inject the repository
        /// </summary>
        /// <param name="repository">entity repository</param>
        public ServiceBase(IRepositoryBase<TEntity> repository) : base()
        {
            _repository = repository;
        }

        /// <summary>
        /// Call the repository to insert a new data at the database
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Add(TEntity entity)
        {
            _repository.Create(entity);
        }

        /// <summary>
        /// Call te repository to insert a range of data at the database
        /// </summary>
        /// <param name="entities">List of Entyties</param>
        public virtual void Add(ICollection<TEntity> entities)
        {
            _repository.Create(entities);
        }

        /// <summary>
        /// Call the repository to count a range of data based on a predicate lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>long</returns>
        public virtual long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Count(predicate).Result;
        }

        /// <summary>
        /// Call the repository to count all the dataset at the database
        /// </summary>
        /// <returns>long</returns>
        public virtual long Count()
        {
            return _repository.Count().Result;
        }

        /// <summary>
        /// Call the repository to get a set of data based on a predicate lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>ICollection</returns>
        public virtual ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.SelectAll(predicate).ToList();
        }

        /// <summary>
        /// Call the repository to get all data of an entity
        /// </summary>
        /// <returns>ICollection</returns>
        public virtual ICollection<TEntity> GetAll()
        {
            return _repository.SelectAll().ToList();
        }

        /// <summary>
        /// Call the repository to get the top 1 row at the database for a specific lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetSingleFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.First(predicate).Result;
        }

        /// <summary>
        /// Call the repository to get the last row at the database for a specific lambda expression
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetSingleLast(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Last(predicate).Result;
        }
        
        /// <summary>
        /// Call the repository to remove an Entity from database
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Remove(TEntity entity)
        {
            _repository.Delete(entity);
        }

        /// <summary>
        /// Call the repository to Update an Entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}

