using DevExpress.DevAV.Common.DataModel;
using DevExpress.Mvvm;
using IGS.Data.Model;

namespace DevExpress.DevAV.DevAVDbDataModel {
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {

        #region inner classes
        public class DbUnitOfWorkFactory : IUnitOfWorkFactory<IPurchasingUnitOfWork> 
        {
            public static readonly IUnitOfWorkFactory<IPurchasingUnitOfWork> Instance = new DbUnitOfWorkFactory();
            private DbUnitOfWorkFactory() { }
            public IPurchasingUnitOfWork CreateUnitOfWork() => new PurchasingUnitOfWork(() => new DevAVDb());
        }

        public class DesignUnitOfWorkFactory : IUnitOfWorkFactory<IPurchasingUnitOfWork>
        {
            public static readonly IUnitOfWorkFactory<IPurchasingUnitOfWork> Instance = new DesignUnitOfWorkFactory();
            private DesignUnitOfWorkFactory() { }
            public IPurchasingUnitOfWork CreateUnitOfWork() => new PurchasingDesignTimeUnitOfWork();
        }

        public class WebApiUnitOfWorkFactory : IUnitOfWorkFactory<IAppSettingsUnitOfWork>
        {
            public static readonly IUnitOfWorkFactory<IAppSettingsUnitOfWork> Instance = new WebApiUnitOfWorkFactory();
            private WebApiUnitOfWorkFactory() { }
            public IAppSettingsUnitOfWork CreateUnitOfWork() => 
                new AppSettingsUnitOfWork(() => new WebApiContext<AppSettings>(() => new AppSettingsSource()));
        }
        
        #endregion

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<IPurchasingUnitOfWork> GetUnitOfWorkFactory() => GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<IPurchasingUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime) => isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;

        public static IUnitOfWorkFactory<IAppSettingsUnitOfWork> GetWebApiUnitOfWorkFactory() => WebApiUnitOfWorkFactory.Instance;

    }
}
