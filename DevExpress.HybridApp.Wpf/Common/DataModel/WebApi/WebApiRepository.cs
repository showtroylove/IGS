using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.DataModel.EntityFramework;
using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.DevAVDbDataModel;

namespace DevExpress.DevAV.Common.DataModel.WebApi
{
    /// <summary>
    /// A DbReadOnlyRepository is a IReadOnlyRepository interface implementation representing the collection of all entities in the unit of work, or that can be queried from the database, of a given type. 
    /// DbReadOnlyRepository objects are created from a DbUnitOfWork using the GetReadOnlyRepository method. 
    /// DbReadOnlyRepository provides only read-only operations against entities of a given type.
    /// </summary>
    /// <typeparam name="TEntity">Repository entity type.</typeparam>
    /// <typeparam name="TWebApiContext">DbContext type.</typeparam>
    public class WebApiReadOnlyRepository<TWebApiContext, TEntity> : DbRepositoryQuery<TEntity>, IReadOnlyRepository<TEntity>
        where TWebApiContext : WebApiContext<TEntity>
        where TEntity : class, new()
        
    {
        private readonly Func<TWebApiContext, Dictionary<TEntity, System.Data.Entity.EntityState>> _dbSetAccessor;
        private readonly WebApiUnitOfWork<TWebApiContext, TEntity> _unitOfWork;

        /// <summary>
        /// Initializes a new instance of DbReadOnlyRepository class.
        /// </summary>
        /// <param name="unitOfWork">Owner unit of work that provides context for repository entities.</param>
        /// <param name="dbSetAccessor">Function that returns DbSet entities from Entity Framework DbContext.</param>
        public WebApiReadOnlyRepository(WebApiUnitOfWork<TWebApiContext, TEntity> unitOfWork, Func<TWebApiContext, Dictionary<TEntity, System.Data.Entity.EntityState>> dbSetAccessor)
            : base(() => dbSetAccessor(unitOfWork.Context).Keys.ToList().AsQueryable())
        {
            _dbSetAccessor = dbSetAccessor;
            _unitOfWork = unitOfWork;
        }

        protected Dictionary<TEntity, System.Data.Entity.EntityState> Entities => _dbSetAccessor(_unitOfWork.Context);

        protected TWebApiContext Context => _unitOfWork.Context;

        #region IReadOnlyRepository
        IUnitOfWork IReadOnlyRepository<TEntity>.UnitOfWork => _unitOfWork;

        #endregion
    }

    /// <summary>
    /// A DbRepository is a IRepository interface implementation representing the collection of all entities in the unit of work, or that can be queried from the database, of a given type. 
    /// DbRepository objects are created from a DbUnitOfWork using the GetRepository method. 
    /// DbRepository provides only write operations against entities of a given type in addition to the read-only operation provided DbReadOnlyRepository base class.
    /// </summary>
    /// <typeparam name="TEntity">Repository entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">Entity primary key type.</typeparam>
    /// <typeparam name="TWebApiContext"></typeparam>
    public class WebApiRepository<TWebApiContext, TEntity, TPrimaryKey> : WebApiReadOnlyRepository<TWebApiContext, TEntity>, IRepository<TEntity, TPrimaryKey>
        where TWebApiContext : WebApiContext<TEntity>
        where TEntity : class, new()
    {
        private readonly Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression;
        private readonly EntityTraits<TEntity, TPrimaryKey> entityTraits;

        /// <summary>
        /// Initializes a new instance of DbRepository class.
        /// </summary>
        /// <param name="unitOfWork">Owner unit of work that provides context for repository entities.</param>
        /// <param name="dbSetAccessor">Function that returns DbSet entities from Entity Framework DbContext.</param>
        /// <param name="getPrimaryKeyExpression">Lambda-expression that returns entity primary key.</param>
        public WebApiRepository(WebApiUnitOfWork<TWebApiContext, TEntity> unitOfWork, Func<TWebApiContext, Dictionary<TEntity, System.Data.Entity.EntityState>> dbSetAccessor, Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression)
            : base(unitOfWork, dbSetAccessor)
        {
            this.getPrimaryKeyExpression = getPrimaryKeyExpression;
            entityTraits = ExpressionHelper.GetEntityTraits(this, getPrimaryKeyExpression);
        }

        protected virtual TEntity CreateCore()
        {
            var newEntity = new TEntity();
            Entities.Add(newEntity, System.Data.Entity.EntityState.Added);
            return newEntity;
        }

        protected virtual void UpdateCore(TEntity entity)
        {
            Entities[entity] = System.Data.Entity.EntityState.Modified;
        }

        protected virtual EntityState GetStateCore(TEntity entity)
        {
            return GetEntityState(Context.Entities[entity]);
        }

        private static EntityState GetEntityState(System.Data.Entity.EntityState entityStates)
        {
            switch (entityStates)
            {
                case System.Data.Entity.EntityState.Added:
                    return EntityState.Added;
                case System.Data.Entity.EntityState.Deleted:
                    return EntityState.Deleted;
                case System.Data.Entity.EntityState.Detached:
                    return EntityState.Detached;
                case System.Data.Entity.EntityState.Modified:
                    return EntityState.Modified;
                case System.Data.Entity.EntityState.Unchanged:
                    return EntityState.Unchanged;
                default:
                    throw new NotImplementedException();
            }
        }

        protected virtual TEntity FindCore(TPrimaryKey primaryKey)
        {
            return Context.Entities.Keys.FirstOrDefault(x =>
            {
                var id = GetPrimaryKeyCore(x);
                return id.Equals(primaryKey);
            });
        }

        protected virtual void RemoveCore(TEntity entity)
        {
            try
            {
                Context.Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw DbExceptionsConverter.Convert(new DbUpdateException("An error occurred while deleting.", ex));
            }
        }

        protected virtual TEntity ReloadCore(TEntity entity)
        {
            return FindCore(GetPrimaryKeyCore(entity));
        }

        protected virtual TPrimaryKey GetPrimaryKeyCore(TEntity entity)
        {
            return entityTraits.GetPrimaryKey(entity);
        }

        protected virtual void SetPrimaryKeyCore(TEntity entity, TPrimaryKey primaryKey)
        {
            var setPrimaryKeyAction = entityTraits.SetPrimaryKey;
            setPrimaryKeyAction(entity, primaryKey);
        }

        #region IRepository
        TEntity IRepository<TEntity, TPrimaryKey>.Find(TPrimaryKey primaryKey)
        {
            return FindCore(primaryKey);
        }

        void IRepository<TEntity, TPrimaryKey>.Remove(TEntity entity)
        {
            RemoveCore(entity);
        }

        TEntity IRepository<TEntity, TPrimaryKey>.Create()
        {
            return CreateCore();
        }

        void IRepository<TEntity, TPrimaryKey>.Update(TEntity entity)
        {
            UpdateCore(entity);
        }

        EntityState IRepository<TEntity, TPrimaryKey>.GetState(TEntity entity)
        {
            return GetStateCore(entity);
        }

        TEntity IRepository<TEntity, TPrimaryKey>.Reload(TEntity entity)
        {
            return ReloadCore(entity);
        }

        Expression<Func<TEntity, TPrimaryKey>> IRepository<TEntity, TPrimaryKey>.GetPrimaryKeyExpression => getPrimaryKeyExpression;

        void IRepository<TEntity, TPrimaryKey>.SetPrimaryKey(TEntity entity, TPrimaryKey primaryKey) => SetPrimaryKeyCore(entity, primaryKey);

        TPrimaryKey IRepository<TEntity, TPrimaryKey>.GetPrimaryKey(TEntity entity) => GetPrimaryKeyCore(entity);

        bool IRepository<TEntity, TPrimaryKey>.HasPrimaryKey(TEntity entity) => entityTraits.HasPrimaryKey(entity);

        #endregion
    }
}
