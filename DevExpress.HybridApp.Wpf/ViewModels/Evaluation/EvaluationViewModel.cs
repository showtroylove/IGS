using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the single Evaluation object view model.
    /// </summary>
    public partial class EvaluationViewModel : SingleObjectViewModel<Evaluation, long, IPurchasingUnitOfWork> {

        /// <summary>
        /// Creates a new instance of EvaluationViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static EvaluationViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EvaluationViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EvaluationViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EvaluationViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected EvaluationViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Evaluations, x => x.Subject) {
        }

        /// <summary>
  /// The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Employee> LookUpEmployees {
            get { return GetLookUpEntitiesViewModel((EvaluationViewModel x) => x.LookUpEmployees, x => x.Employees); }
        }
    }
}
