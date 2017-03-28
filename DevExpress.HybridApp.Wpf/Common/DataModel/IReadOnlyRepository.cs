
namespace DevExpress.DevAV.Common.DataModel {
    /// <summary>
    /// The IReadOnlyRepository interface represents the read-only implementation of the Repository pattern 
    /// such that it can be used to query entities of a given type. 
    /// </summary>
    /// <typeparam name="TEntity">Repository entity type.</typeparam>
    public interface IReadOnlyRepository<TEntity> : IRepositoryQuery<TEntity> where TEntity : class 
    {
        /// <summary>
        /// The owner unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
