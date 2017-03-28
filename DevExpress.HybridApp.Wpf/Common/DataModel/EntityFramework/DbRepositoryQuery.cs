using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DevExpress.DevAV.Common.DataModel.EntityFramework
{
    /// <summary>
    /// DbRepositoryQuery is an IRepositoryQuery interface implementation that is an extension of IQueryable designed to specify the related objects to include in query results.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    public class DbRepositoryQuery<TEntity> : RepositoryQueryBase<TEntity>, IRepositoryQuery<TEntity> where TEntity : class 
    {
        /// <summary>
        /// Initializes a new instance of the DesignTimeRepositoryQuery class.
        /// </summary>
        /// <param name="getQueryable">A function that returns an IQueryable instance which is used by DbRepositoryQuery to perform queries.</param>
        public DbRepositoryQuery(Func<IQueryable<TEntity>> getQueryable)
            : base(getQueryable) { }

        IRepositoryQuery<TEntity> IRepositoryQuery<TEntity>.Include<TProperty>(Expression<Func<TEntity, TProperty>> path) => new DbRepositoryQuery<TEntity>(() => Queryable.Include(path));

        IRepositoryQuery<TEntity> IRepositoryQuery<TEntity>.Where(Expression<Func<TEntity, bool>> predicate) => new DbRepositoryQuery<TEntity>(() => Queryable.Where(predicate));
    }
}