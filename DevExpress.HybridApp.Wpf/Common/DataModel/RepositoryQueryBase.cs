using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevExpress.DevAV.Common.DataModel
{
    /// <summary>
    /// The base class that helps to implement the IRepositoryQuery interface as a wrapper over an existing IQuerable instance.
    /// </summary>
    /// <typeparam name="T">An entity type.</typeparam>
    public abstract class RepositoryQueryBase<T> : IQueryable<T> 
    {
        private readonly Lazy<IQueryable<T>> queryable;
        protected IQueryable<T> Queryable => queryable.Value;

        protected RepositoryQueryBase(Func<IQueryable<T>> getQueryable) 
        {
            queryable = new Lazy<IQueryable<T>>(getQueryable);
        }
        Type IQueryable.ElementType => Queryable.ElementType;
        Expression IQueryable.Expression => Queryable.Expression;
        IQueryProvider IQueryable.Provider => Queryable.Provider;
        IEnumerator IEnumerable.GetEnumerator() => Queryable.GetEnumerator(); 
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => Queryable.GetEnumerator(); 
    }
}