using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the Orders collection view model.
    /// </summary>
    public partial class OrderCollectionViewModel : CollectionViewModel<Order, long, IPurchasingUnitOfWork> {

        /// <summary>
        /// Creates a new instance of OrderCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static OrderCollectionViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new OrderCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the OrderCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the OrderCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected OrderCollectionViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Orders, query => query.Include(x => x.Store).Include(x => x.Customer).ActualOrders()) {
        }
    }
}
