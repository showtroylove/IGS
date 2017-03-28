using System;
using System.Linq.Expressions;
using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the Employees collection view model.
    /// </summary>
    public partial class AppSettingsCollectionViewModel : CollectionViewModel<IGS.Data.Model.AppSettings, int, IAppSettingsUnitOfWork>
        ,ISupportFiltering<IGS.Data.Model.AppSettings>, IFilterTreeViewModelContainer<IGS.Data.Model.AppSettings, int>
    {

        /// <summary>
        /// Creates a new instance of EmployeeCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AppSettingsCollectionViewModel Create(IUnitOfWorkFactory<IAppSettingsUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new AppSettingsCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EmployeeCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EmployeeCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AppSettingsCollectionViewModel(IUnitOfWorkFactory<IAppSettingsUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetWebApiUnitOfWorkFactory(), x => x.AppSettings) {
        }

        private IDocumentManagerService DocumentManagerService => this.GetRequiredService<IDocumentManagerService>();
        private IDocumentManagerService EditDetailsDocumentManagerService => this.GetRequiredService<IDocumentManagerService>("EditDetailsDocumentManagerService");

        public virtual FilterTreeViewModel<IGS.Data.Model.AppSettings, int> FilterTreeViewModel { get; set; }

        #region ISupportFiltering
        Expression<Func<IGS.Data.Model.AppSettings, bool>> ISupportFiltering<IGS.Data.Model.AppSettings>.FilterExpression
        {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}
