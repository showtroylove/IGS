using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the Evaluations collection view model.
    /// </summary>
    public partial class EvaluationCollectionViewModel : CollectionViewModel<Evaluation, long, IPurchasingUnitOfWork> {

        /// <summary>
        /// Creates a new instance of EvaluationCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static EvaluationCollectionViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EvaluationCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EvaluationCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EvaluationCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected EvaluationCollectionViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Evaluations) {
        }
    }
}
