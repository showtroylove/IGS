using DevExpress.DevAV.Common.DataModel;

namespace DevExpress.DevAV.DevAVDbDataModel {
    /// <summary>
    /// A DevAVDbDesignTimeUnitOfWork instance that represents the design-time implementation of the IDevAVDbUnitOfWork interface.
    /// </summary>
    public class PurchasingDesignTimeUnitOfWork : DesignTimeUnitOfWork, IPurchasingUnitOfWork {

        //IRepository<ItemsSource, int> IDevAVDbUnitOfWork.ItemsSource { get; }

        IRepository<CustomerCommunication, long> IPurchasingUnitOfWork.Communications {
            get { return GetRepository((CustomerCommunication x)=>x.Id); }
        }

        IRepository<CustomerEmployee, long> IPurchasingUnitOfWork.CustomerEmployees {
            get { return GetRepository((CustomerEmployee x)=>x.Id); }
        }

        IRepository<Customer, long> IPurchasingUnitOfWork.Customers {
            get { return GetRepository((Customer x)=>x.Id); }
        }

        IRepository<CustomerStore, long> IPurchasingUnitOfWork.CustomerStores {
            get { return GetRepository((CustomerStore x)=>x.Id); }
        }

        IRepository<Crest, long> IPurchasingUnitOfWork.Crests {
            get { return GetRepository((Crest x)=>x.Id); }
        }

        IRepository<Order, long> IPurchasingUnitOfWork.Orders {
            get { return GetRepository((Order x)=>x.Id); }
        }

        IRepository<Employee, long> IPurchasingUnitOfWork.Employees {
            get { return GetRepository((Employee x)=>x.Id); }
        }

        IRepository<EmployeeTask, long> IPurchasingUnitOfWork.Tasks {
            get { return GetRepository((EmployeeTask x)=>x.Id); }
        }

        IRepository<Evaluation, long> IPurchasingUnitOfWork.Evaluations {
            get { return GetRepository((Evaluation x)=>x.Id); }
        }

        IRepository<Picture, long> IPurchasingUnitOfWork.Pictures {
            get { return GetRepository((Picture x)=>x.Id); }
        }

        IRepository<Probation, long> IPurchasingUnitOfWork.Probations {
            get { return GetRepository((Probation x)=>x.Id); }
        }

        IRepository<OrderItem, long> IPurchasingUnitOfWork.OrderItems {
            get { return GetRepository((OrderItem x)=>x.Id); }
        }

        IRepository<Product, long> IPurchasingUnitOfWork.Products {
            get { return GetRepository((Product x)=>x.Id); }
        }

        IRepository<ProductCatalog, long> IPurchasingUnitOfWork.ProductCatalogs {
            get { return GetRepository((ProductCatalog x)=>x.Id); }
        }

        IRepository<ProductImage, long> IPurchasingUnitOfWork.ProductImages {
            get { return GetRepository((ProductImage x)=>x.Id); }
        }

        IRepository<Quote, long> IPurchasingUnitOfWork.Quotes {
            get { return GetRepository((Quote x)=>x.Id); }
        }

        IRepository<QuoteItem, long> IPurchasingUnitOfWork.QuoteItems {
            get { return GetRepository((QuoteItem x)=>x.Id); }
        }

        IRepository<State, StateEnum> IPurchasingUnitOfWork.States {
            get { return GetRepository((State x)=>x.ShortName); }
        }

        IRepository<DatabaseVersion, long> IPurchasingUnitOfWork.Version {
            get { return GetRepository((DatabaseVersion x)=>x.Id); }
        }
    }
}
