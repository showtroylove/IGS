using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevExpress.DevAV.Common.DataModel
{
    /// <summary>
    /// The IRepositoryQuery interface represents an extension of IQueryable designed to provide an ability to specify the related objects to include in the query results.
    /// </summary>
    /// <typeparam name="T">An entity type.</typeparam>
    public interface IRepositoryQuery<T> : IQueryable<T> 
    {
        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// </summary>
        /// <typeparam name="TProperty">The type of the navigation property to be included.</typeparam>
        /// <param name="path">A lambda expression that represents the path to include.</param>
        IRepositoryQuery<T> Include<TProperty>(Expression<Func<T, TProperty>> path);

        /// <summary>
        /// Filters a sequence of entities based on the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each entity for a condition.</param>
        IRepositoryQuery<T> Where(Expression<Func<T, bool>> predicate);
    }
}