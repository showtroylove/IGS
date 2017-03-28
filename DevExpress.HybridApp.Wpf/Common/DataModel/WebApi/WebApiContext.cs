using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using DevExpress.DevAV.Common.DataModel;
using EntityState = System.Data.Entity.EntityState;
using DevExpress.DevAV.Common.DataModel.WebApi;
using DevExpress.Mvvm.Native;

namespace DevExpress.DevAV.DevAVDbDataModel
{
    public class WebApiContext<TEntity> : IUnitOfWork, IDisposable where TEntity : class, new() 
    {
        private CancellationTokenSource _cts;
        private readonly Lazy<WebApiSourceBase<TEntity>> _webSource;

        public WebApiContext(Func<WebApiSourceBase<TEntity>> websourceFactory)
        {
            _webSource = new Lazy<WebApiSourceBase<TEntity>>(websourceFactory);
        }
        
        public WebApiSourceBase<TEntity> WebApiSource => _webSource.Value;

        private readonly Dictionary<TEntity, EntityState> _entities = new Dictionary<TEntity, EntityState>();

        public Dictionary<TEntity, EntityState> Entities
        {
            get
            {
                if (!IsLoaded)
                    LoadEntitiesAsync();
                return _entities;
            }
        }

        public void LoadEntitiesAsync(bool forceLoad = false)
        {
            if (forceLoad)
            {
                _cts?.Cancel();
            }
            else if (IsLoading)
            {
                return;
            }

            _cts = LoadCoreAsync().Result;
            //LoadCoreAsync().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //        return;
                
            //    _cts = task.Result;
            //});
        }

        private async Task<CancellationTokenSource> LoadCoreAsync()
        {
            IsLoading = true;
            var cts = new CancellationTokenSource();
            WebApiSource.JsonRestClient.CancelToken = cts.Token;
            var entities = await WebApiSource.JsonRestClient.GetAllAsync();

            IsLoading = false;
            entities.ForEach(e => _entities.Add(e, EntityState.Unchanged));
            return cts;
        }

        protected bool IsLoading { get; set; }
        protected bool IsLoaded => _entities.Any();
        
        public void SaveChanges() => WebApiSource.SaveAllCore(Entities);

        public virtual bool HasChanges() => Entities.Values.Any(x => x != EntityState.Unchanged);

        public virtual void CancelLoad()
        {
            _cts?.Cancel();
            IsLoading = false;
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_webSource.IsValueCreated)
                        _webSource.Value.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WebApiContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}