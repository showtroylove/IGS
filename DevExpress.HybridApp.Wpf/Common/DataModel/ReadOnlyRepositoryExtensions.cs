using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevExpress.DevAV.Common.DataModel
{
    /// <summary>
    /// Provides a set of extension methods to perform commonly used operations with IReadOnlyRepository.
    /// </summary>
    public static class ReadOnlyRepositoryExtensions 
    {

        /// <summary>
        /// Returns IQuerable representing sequence of entities from repository filtered by the given predicate and projected to the specified projection entity type by the given LINQ function.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="projection">A LINQ function used to transform entities from repository entity type to projection entity type.</param>
        public static IQueryable<TProjection> GetFilteredEntities<TEntity, TProjection>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection) where TEntity : class 
        {
            var filtered = predicate != null ? repository.Where(predicate) : repository;
            return projection != null ? projection(filtered) : (IQueryable<TProjection>)filtered;
        }

        /// <summary>
        /// Returns IQuerable representing sequence of entities from repository filtered by the given predicate.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public static IQueryable<TEntity> GetFilteredEntities<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate) where TEntity : class => repository.GetFilteredEntities(predicate, x => x);
    }
}