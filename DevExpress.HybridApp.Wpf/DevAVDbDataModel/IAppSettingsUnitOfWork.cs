using DevExpress.DevAV.Common.DataModel;
using IGS.Data.Model;

namespace DevExpress.DevAV.DevAVDbDataModel
{
    public interface IAppSettingsUnitOfWork : IUnitOfWork
    {
        IRepository<AppSettings, int> AppSettings { get; }
    }
}
