using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.Common.DataModel;

namespace DevExpress.DevAV.Common.ViewModel {
    /// <summary>
    /// The base class for POCO view models exposing a collection of entities of the given type.
    /// This is a partial class that provides an extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public abstract partial class EntitiesViewModel<TEntity, TProjection, TUnitOfWork> :
        EntitiesViewModelBase<TEntity, TProjection, TUnitOfWork>
        where TEntity : class
        where TProjection : class
        where TUnitOfWork : IUnitOfWork 
    {

        /// <summary>
        /// Initializes a new instance of the EntitiesViewModel class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        protected EntitiesViewModel(
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IReadOnlyRepository<TEntity>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection)
            : base(unitOfWorkFactory, getRepositoryFunc, projection) 
        {
        }
    }

    /// <summary>
    /// The base class for a POCO view models exposing a collection of entities of the given type.
    /// It is not recommended to inherit directly from this class. Use the EntitiesViewModel class instead.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    [POCOViewModel]
    public abstract class EntitiesViewModelBase<TEntity, TProjection, TUnitOfWork> : IEntitiesViewModel<TProjection>
        where TEntity : class
        where TProjection : class
        where TUnitOfWork : IUnitOfWork 
    {

        #region inner classes
        protected interface IEntitiesChangeTracker 
        {
            void RegisterMessageHandler();
            void UnregisterMessageHandler();
        }

        protected class EntitiesChangeTracker<TPrimaryKey> : IEntitiesChangeTracker 
        {
            private readonly EntitiesViewModelBase<TEntity, TProjection, TUnitOfWork> owner;
            private ObservableCollection<TProjection> Entities => owner.Entities;
            private IRepository<TEntity, TPrimaryKey> Repository => (IRepository<TEntity, TPrimaryKey>)owner.ReadOnlyRepository;

            public EntitiesChangeTracker(EntitiesViewModelBase<TEntity, TProjection, TUnitOfWork> owner) 
            {
                this.owner = owner;
            }

            void IEntitiesChangeTracker.RegisterMessageHandler() => Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(this, OnMessage);

            void IEntitiesChangeTracker.UnregisterMessageHandler() => Messenger.Default.Unregister(this);

            public TProjection FindLocalProjectionByKey(TPrimaryKey primaryKey) 
            {
                var primaryKeyEqualsExpression = RepositoryExtensions.GetProjectionPrimaryKeyEqualsExpression<TEntity, TProjection, TPrimaryKey>(Repository, primaryKey);
                return Entities.AsQueryable().FirstOrDefault(primaryKeyEqualsExpression);
            }

            public TProjection FindActualProjectionByKey(TPrimaryKey primaryKey) 
            {
                var projectionEntity = Repository.FindActualProjectionByKey(owner.Projection, primaryKey);
                if (projectionEntity == null ||
                    !ExpressionHelper.IsFitEntity(Repository.Find(primaryKey), owner.GetFilterExpression()))
                    return null;
                owner.OnEntitiesLoaded(GetUnitOfWork(Repository), new [] { projectionEntity });
                return projectionEntity;
            }

            private void OnMessage(EntityMessage<TEntity, TPrimaryKey> message) 
            {
                if(!owner.IsLoaded)
                    return;
                switch(message.MessageType) 
                {
                    case EntityMessageType.Added:
                        OnEntityAdded(message.PrimaryKey);
                        break;
                    case EntityMessageType.Changed:
                        OnEntityChanged(message.PrimaryKey);
                        break;
                    case EntityMessageType.Deleted:
                        OnEntityDeleted(message.PrimaryKey);
                        break;
                }
            }

            private void OnEntityAdded(TPrimaryKey primaryKey) 
            {
                var projectionEntity = FindActualProjectionByKey(primaryKey);
                if(projectionEntity != null)
                    Entities.Add(projectionEntity);
            }

            private void OnEntityChanged(TPrimaryKey primaryKey) 
            {
                var existingProjectionEntity = FindLocalProjectionByKey(primaryKey);
                var projectionEntity = FindActualProjectionByKey(primaryKey);
                if(projectionEntity == null) {
                    Entities.Remove(existingProjectionEntity);
                    return;
                }
                if(existingProjectionEntity != null) {
                    Entities[Entities.IndexOf(existingProjectionEntity)] = projectionEntity;
                    owner.RestoreSelectedEntity(existingProjectionEntity, projectionEntity);
                    return;
                }
                OnEntityAdded(primaryKey);
            }

            private void OnEntityDeleted(TPrimaryKey primaryKey) => Entities.Remove(FindLocalProjectionByKey(primaryKey));
        }
        #endregion

        private ObservableCollection<TProjection> entities = new ObservableCollection<TProjection>();
        private CancellationTokenSource loadCancellationTokenSource;
        protected readonly IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory;
        protected readonly Func<TUnitOfWork, IReadOnlyRepository<TEntity>> getRepositoryFunc;
        protected Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> Projection { get; private set; }

        /// <summary>
        /// Initializes a new instance of the EntitiesViewModelBase class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        protected EntitiesViewModelBase(
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IReadOnlyRepository<TEntity>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection
            ) 
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.getRepositoryFunc = getRepositoryFunc;
            Projection = projection;
            ChangeTracker = CreateEntitiesChangeTracker();
            if(!this.IsInDesignMode())
                OnInitializeInRuntime();
        }

        /// <summary>
        /// Used to check whether entities are currently being loaded in the background. The property can be used to show the progress indicator.
        /// </summary>
        public virtual bool IsLoading { get; protected set; }

        /// <summary>
        /// The collection of entities loaded from the unit of work.
        /// </summary>
        public ObservableCollection<TProjection> Entities 
        {
            get 
            {
                if(!IsLoaded)
                    LoadEntities(false);
                return entities;
            }
        }

        protected IEntitiesChangeTracker ChangeTracker { get; private set; }

        protected IReadOnlyRepository<TEntity> ReadOnlyRepository { get; private set; }

        protected bool IsLoaded => ReadOnlyRepository != null;

        protected void LoadEntities(bool forceLoad) 
        {
            if(forceLoad)
            {
                loadCancellationTokenSource?.Cancel();
            }
            else if(IsLoading) 
            {
                return;
            }
            loadCancellationTokenSource = LoadCore();
        }

        private void CancelLoading() 
        {
            loadCancellationTokenSource?.Cancel();
            IsLoading = false;
        }

        private CancellationTokenSource LoadCore()
        {
            IsLoading = true;
            var cancellationTokenSource = new CancellationTokenSource();
            var selectedEntityCallback = GetSelectedEntityCallback();
            Task.Factory.StartNew(() =>
            {
                var repository = CreateReadOnlyRepository();
                var entities =
                    new ObservableCollection<TProjection>(repository.GetFilteredEntities(GetFilterExpression(),
                        Projection));
                OnEntitiesLoaded(GetUnitOfWork(repository), entities);
                return (repo: repository, entities: entities);
            }).ContinueWith(x =>
                {
                    if (!x.IsFaulted)
                    {
                        ReadOnlyRepository = x.Result.repo;
                        entities = x.Result.entities;
                        this.RaisePropertyChanged(y => y.Entities);
                        OnEntitiesAssigned(selectedEntityCallback);
                    }
                    IsLoading = false;
                }, cancellationTokenSource.Token, TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
            return cancellationTokenSource;
        }

        private static TUnitOfWork GetUnitOfWork(IReadOnlyRepository<TEntity> repository) 
        {
            return (TUnitOfWork)repository.UnitOfWork;
        }

        protected virtual void OnEntitiesLoaded(TUnitOfWork unitOfWork, IEnumerable<TProjection> entities) 
        {
        }

        protected virtual void OnEntitiesAssigned(Func<TProjection> getSelectedEntityCallback) 
        {
        }

        protected virtual Func<TProjection> GetSelectedEntityCallback() => null;

        protected virtual void RestoreSelectedEntity(TProjection existingProjectionEntity, TProjection projectionEntity) 
        {
        }

        protected virtual Expression<Func<TEntity, bool>> GetFilterExpression() => null;

        protected virtual void OnInitializeInRuntime() => ChangeTracker?.RegisterMessageHandler();

        protected virtual void OnDestroy() 
        {
            CancelLoading();
            ChangeTracker?.UnregisterMessageHandler();
        }

        protected virtual void OnIsLoadingChanged() 
        {
        }

        protected IReadOnlyRepository<TEntity> CreateReadOnlyRepository() => getRepositoryFunc(CreateUnitOfWork());

        protected TUnitOfWork CreateUnitOfWork() 
        {
            return unitOfWorkFactory.CreateUnitOfWork();
        }

        protected virtual IEntitiesChangeTracker CreateEntitiesChangeTracker() {
            return null;
        }

        protected IDocumentOwner DocumentOwner { get; private set; }

        #region IDocumentContent
        object IDocumentContent.Title => null;

        void IDocumentContent.OnClose(CancelEventArgs e) { }

        void IDocumentContent.OnDestroy() => OnDestroy();

        IDocumentOwner IDocumentContent.DocumentOwner 
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

        #region IEntitiesViewModel
        ObservableCollection<TProjection> IEntitiesViewModel<TProjection>.Entities => Entities;

        bool IEntitiesViewModel<TProjection>.IsLoading => IsLoading;

        #endregion
    }

    /// <summary>
    /// The base interface for view models exposing a collection of entities of the given type.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    public interface IEntitiesViewModel<TEntity> : IDocumentContent where TEntity : class 
    {
        /// <summary>
        /// The loaded collection of entities.
        /// </summary>
        ObservableCollection<TEntity> Entities { get; }

        /// <summary>
        /// Used to check whether entities are currently being loaded in the background. The property can be used to show the progress indicator.
        /// </summary>
        bool IsLoading { get; }
    }
}
