using System;
using System.Collections.Generic;
using System.Net.Http;
using IGS.Data;

namespace DevExpress.DevAV.Common.DataModel.WebApi
{
    public abstract class WebApiSourceBase<TEntity>: IDisposable 
        where TEntity : class, new()
    {
        private readonly Lazy<HttpClient> _webapiClient;

        protected WebApiSourceBase(Func<HttpClient> clientFactory) 
        {
            _webapiClient = new Lazy<HttpClient>(clientFactory);
            JsonRestClient = new JsonRestClient<TEntity>(WebClient, WebClient.BaseAddress);
        }

        protected Dictionary<System.Data.Entity.EntityState, string> Routes { get; } = new Dictionary<System.Data.Entity.EntityState, string>();
        protected HttpClient WebClient => _webapiClient.Value;
        protected abstract Dictionary<System.Data.Entity.EntityState, string> GetRoutes();
        public JsonRestClient<TEntity> JsonRestClient { get; }

        public void SaveAllCore(Dictionary<TEntity, System.Data.Entity.EntityState> uncommited)
        {
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
                    if (_webapiClient.IsValueCreated)
                        _webapiClient.Value.Dispose();
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