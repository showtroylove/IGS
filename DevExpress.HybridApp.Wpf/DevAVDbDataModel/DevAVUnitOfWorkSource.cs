using DevExpress.DevAV.Common.DataModel;
using DevExpress.Mvvm;

namespace DevExpress.DevAV.DevAVDbDataModel {
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {

        #region inner classes
            public class DbUnitOfWorkFactory : IUnitOfWorkFactory<IDevAVDbUnitOfWork> 
            {
                public static readonly IUnitOfWorkFactory<IDevAVDbUnitOfWork> Instance = new DbUnitOfWorkFactory();
                private DbUnitOfWorkFactory() { }
                public IDevAVDbUnitOfWork CreateUnitOfWork() => new DevAVDbUnitOfWork(() => new DevAVDb());
            }

            public class DesignUnitOfWorkFactory : IUnitOfWorkFactory<IDevAVDbUnitOfWork>
            {
                public static readonly IUnitOfWorkFactory<IDevAVDbUnitOfWork> Instance = new DesignUnitOfWorkFactory();
                private DesignUnitOfWorkFactory() { }
                public IDevAVDbUnitOfWork CreateUnitOfWork() => new DevAVDbDesignTimeUnitOfWork();
            }
        #endregion

  /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<IDevAVDbUnitOfWork> GetUnitOfWorkFactory() => GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<IDevAVDbUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime) => isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;
    }
}
