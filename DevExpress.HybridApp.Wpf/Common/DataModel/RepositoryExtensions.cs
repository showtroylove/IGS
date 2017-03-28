using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.Utils;

namespace DevExpress.DevAV.Common.DataModel
{
    /// <summary>
    /// Provides a set of extension methods to perform commonly used operations with IRepository.
    /// </summary>
    public static class RepositoryExtensions {

        /// <summary>
        /// Builds a lambda expression that compares an entity primary key with the given constant value.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="primaryKey">A value to compare with the entity primary key.</param>
        public static Expression<Func<TEntity, bool>> GetPrimaryKeyEqualsExpression<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey primaryKey) where TEntity : class {
            return ExpressionHelper.GetValueEqualsExpression(repository.GetPrimaryKeyExpression, primaryKey);
        }

        /// <summary>
        /// Builds a lambda expression that compares an entity primary key with the given constant value.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="primaryKey">A value to compare with the entity primary key.</param>
        public static Expression<Func<TProjection, bool>> GetProjectionPrimaryKeyEqualsExpression<TEntity, TProjection, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey primaryKey) where TEntity : class {
            return GetProjectionValue(primaryKey,
                (TPrimaryKey x) => repository.GetPrimaryKeyEqualsExpression(x),
                (TPrimaryKey x) => GetProjectionPrimaryKeyEqualsExpressionCore<TEntity, TProjection, TPrimaryKey>(repository, x));
        }

        /// <summary>
        /// Returns a primary key of the given entity.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="projectionEntity">An entity.</param>
        public static TPrimaryKey GetProjectionPrimaryKey<TEntity, TProjection, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TProjection projectionEntity) where TEntity : class {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => repository.GetPrimaryKey(x),
                (TProjection x) => (TPrimaryKey)TypeDescriptor.GetProperties(typeof(TProjection))[repository.GetPrimaryKeyPropertyName()].GetValue(x));
        }

        /// <summary>
        /// Gets whether the given entity is detached from the unit of work.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="projectionEntity">An entity.</param>
        public static bool IsDetached<TEntity, TProjection, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TProjection projectionEntity) where TEntity : class {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => repository.GetState(x) == EntityState.Detached,
                (TProjection x) => false);
        }

        /// <summary>
        /// Determines whether the given entity has the primary key assigned (the primary key is not null). Always returns true if the primary key is a non-nullable value type.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="projectionEntity">An entity.</param>
        public static bool ProjectionHasPrimaryKey<TEntity, TProjection, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TProjection projectionEntity) where TEntity : class {
            return GetProjectionValue(projectionEntity,
                (TEntity x) => repository.HasPrimaryKey(x),
                (TProjection x) => true);
        }

        /// <summary>
        /// Loads from the store or updates an entity with the given primary key value. If no entity with the given primary key is found in the store, returns null.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="projection">A LINQ function used to transform entities from the repository entity type to the projection entity type.</param>
        /// <param name="primaryKey">A value to compare with the entity primary key.</param>
        public static TProjection FindActualProjectionByKey<TEntity, TProjection, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection, TPrimaryKey primaryKey) where TEntity : class {
            var primaryKeyEqualsExpression = GetProjectionPrimaryKeyEqualsExpression<TEntity, TProjection, TPrimaryKey>(repository, primaryKey);
            var result = repository.GetFilteredEntities(null, projection).Where(primaryKeyEqualsExpression).Take(1).ToArray().FirstOrDefault();
            return GetProjectionValue(result,
                (TEntity x) => x != null ? repository.Reload(x) : null,
                (TProjection x) => x);
        }

        /// <summary>
        /// Returns an entity primary key property name.
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TPrimaryKey">A primary key type.</typeparam>
        /// <param name="repository">A repository.</param>
        public static string GetPrimaryKeyPropertyName<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository) where TEntity : class {
            return ExpressionHelper.GetPropertyName(repository.GetPrimaryKeyExpression);
        }

        private static TProjectionResult GetProjectionValue<TEntity, TProjection, TEntityResult, TProjectionResult>(TProjection value, Func<TEntity, TEntityResult> entityFunc, Func<TProjection, TProjectionResult> projectionFunc) {
            if(typeof(TEntity) != typeof(TProjection) || typeof(TEntityResult) != typeof(TProjectionResult))
                return projectionFunc(value);
            return (TProjectionResult)(object)entityFunc((TEntity)(object)value);
        }

        private static Expression<Func<TProjection, bool>> GetProjectionPrimaryKeyEqualsExpressionCore<TEntity, TProjection, TPrimaryKey>(IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey primaryKey) where TEntity : class {
            var parameter = Expression.Parameter(typeof(TProjection));
            var keyExpression = Expression.Lambda<Func<TProjection, TPrimaryKey>>(Expression.Property(parameter, repository.GetPrimaryKeyPropertyName()), parameter);
            return ExpressionHelper.GetValueEqualsExpression(keyExpression, primaryKey);
        }
    }
}