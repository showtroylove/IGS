using System;
using DevExpress.DevAV.Common.DataModel;
using IGS.Data.Model;

namespace DevExpress.DevAV.DevAVDbDataModel
{
    public class AppSettingsUnitOfWork : WebApiUnitOfWork<WebApiContext<AppSettings>, AppSettings>, IAppSettingsUnitOfWork
    {
        public AppSettingsUnitOfWork(Func<WebApiContext<AppSettings>> contextFactory) : base(contextFactory)
        {
        }
        
        IRepository<AppSettings, int> IAppSettingsUnitOfWork.AppSettings => GetRepository(context => context.Entities, x => x.AppId);
    }
}