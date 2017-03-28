using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the single Customer object view model.
    /// </summary>
    public partial class CustomerViewModel : SingleObjectViewModel<Customer, long, IPurchasingUnitOfWork> {

        /// <summary>
        /// Creates a new instance of CustomerViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustomerViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new CustomerViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CustomerViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustomerViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Customers, x => x.Name) {
        }

        /// <summary>
        /// The view model for the CustomerEmployees detail collection.
        /// </summary>
        public CollectionViewModel<CustomerEmployee, long, IPurchasingUnitOfWork> CustomerEmployeesDetails {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerEmployeesDetails, x => x.CustomerEmployees, x => x.CustomerId, (x, key) => x.CustomerId = key); }
        }

        /// <summary>
        /// The view model for the CustomerOrders detail collection.
        /// </summary>
        public CollectionViewModel<Order, long, IPurchasingUnitOfWork> CustomerOrdersDetails {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerOrdersDetails, x => x.Orders, x => x.CustomerId, (x, key) => x.CustomerId = key, query => query.ActualOrders()); }
        }

        /// <summary>
        /// The view model for the CustomerQuotes detail collection.
        /// </summary>
        public CollectionViewModel<Quote, long, IPurchasingUnitOfWork> CustomerQuotesDetails {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerQuotesDetails, x => x.Quotes, x => x.CustomerId, (x, key) => x.CustomerId = key, query => query.ActualQuotes()); }
        }

        /// <summary>
        /// The view model for the CustomerCustomerStores detail collection.
        /// </summary>
        public CollectionViewModel<CustomerStore, long, IPurchasingUnitOfWork> CustomerCustomerStoresDetails {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerCustomerStoresDetails, x => x.CustomerStores, x => x.CustomerId, (x, key) => x.CustomerId = key); }
        }
    }
}
