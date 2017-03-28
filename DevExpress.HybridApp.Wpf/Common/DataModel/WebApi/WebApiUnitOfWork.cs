using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.DataModel.EntityFramework;
using DevExpress.DevAV.Common.DataModel.WebApi;

namespace DevExpress.DevAV.DevAVDbDataModel
{
    public abstract class WebApiUnitOfWork<TWebApiContext, TEntity> : UnitOfWorkBase, IUnitOfWork, IDisposable
        where TWebApiContext : WebApiContext<TEntity>
        where TEntity : class, new()
    {
        private readonly Lazy<TWebApiContext> _context;

        protected WebApiUnitOfWork(Func<TWebApiContext> contextFactory)
        {
            _context = new Lazy<TWebApiContext>(contextFactory);
        }

        /// <summary>
        /// Instance of underlying DbContext.
        /// </summary>
        public TWebApiContext Context => _context.Value;

        void IUnitOfWork.SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw DbExceptionsConverter.Convert(new DbUpdateException("An error occurred while saving.", ex));
            }
        }

        bool IUnitOfWork.HasChanges() => Context.HasChanges();

        protected IRepository<TEntity, TPrimaryKey> GetRepository<TPrimaryKey>(Func<TWebApiContext, Dictionary<TEntity, System.Data.Entity.EntityState>> dbSetAccessor, 
                                                                               Expression<Func<TEntity, TPrimaryKey>> getPrimaryKeyExpression) 
            => GetRepositoryCore<IRepository<TEntity, TPrimaryKey>, TEntity>(() => new WebApiRepository<TWebApiContext, TEntity, TPrimaryKey>(this, dbSetAccessor, getPrimaryKeyExpression));

        protected IReadOnlyRepository<TEntity> GetReadOnlyRepository(Func<TWebApiContext, Dictionary<TEntity, System.Data.Entity.EntityState>> dbSetAccessor) 
            => GetRepositoryCore<IReadOnlyRepository<TEntity>, TEntity>(() => new WebApiReadOnlyRepository<TWebApiContext, TEntity>(this, dbSetAccessor));

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_context.IsValueCreated)
                        _context.Value.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WebApiUnitOfWork() {
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

    //public class WebApiUnitOfWork<TWebApiContext>  : IUnitOfWork 
    //    where TEntity: class, new()
    //{
    //    private static readonly CancellationTokenSource Cts = new CancellationTokenSource();

    //    public static async Task<IEnumerable<TEntity>> JsonRestHelper(CancellationToken token, string action = null )
    //    {
    //        var baseadd = (Uri)Default[Default.CurrentService] ?? Default.EnvironmentUri;
    //        using (var handler = new HttpClientHandler
    //        {
    //            CookieContainer = new CookieContainer(),
    //            PreAuthenticate = true,
    //            UseDefaultCredentials = true,
    //            UseCookies = true
    //        })
    //        using (var client = new HttpClient(handler) {BaseAddress = baseadd})
    //        {
    //            var json = new JsonRestClient<TEntity>(client, client.BaseAddress) {CancelToken = token};
    //            var results = await json.GetAllAsync($"{client.BaseAddress}/{action}");
    //            return results;
    //        }
    //    }

    //    public static IEnumerable<TEntity> GetAppSettings()
    //    {
    //        var results = Task.Run(()=> JsonRestHelper(Cts.Token));
    //        return results.Result;
    //    }

    //    public void SaveChanges()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool HasChanges() => false;
    //}
}
