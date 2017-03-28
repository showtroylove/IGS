using System;
using System.Collections.Generic;

namespace DevExpress.DevAV.Common.DataModel {
    /// <summary>
    /// The base class for unit of works that provides the storage for repositories. 
    /// </summary>
    public class UnitOfWorkBase
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected TRepository GetRepositoryCore<TRepository, TEntity>(Func<TRepository> createRepositoryFunc)
            where TRepository : IReadOnlyRepository<TEntity>
            where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out object result)) return (TRepository) result;
            result = createRepositoryFunc();
            _repositories[typeof(TEntity)] = result;
            return (TRepository) result;
        }
    }
}
