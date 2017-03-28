using System;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.DataModel.EntityFramework;

namespace DevExpress.DevAV.DevAVDbDataModel {
    /// <summary>
        /// A DevAVDbUnitOfWork instance that represents the run-time implementation of the IDevAVDbUnitOfWork interface.
        /// </summary>
        public class PurchasingUnitOfWork : DbUnitOfWork<DevAVDb>, IPurchasingUnitOfWork 
    {
        public PurchasingUnitOfWork(Func<DevAVDb> contextFactory): base(contextFactory) 
        {
        }

        //IRepository<ItemsSource, int> IDevAVDbUnitOfWork.ItemsSource => GetRepository()

        IRepository<CustomerCommunication, long> IPurchasingUnitOfWork.Communications => GetRepository(x => x.Set<CustomerCommunication>(), x=>x.Id);

        IRepository<CustomerEmployee, long> IPurchasingUnitOfWork.CustomerEmployees => GetRepository(x => x.Set<CustomerEmployee>(), x=>x.Id);

        IRepository<Customer, long> IPurchasingUnitOfWork.Customers => GetRepository(x => x.Set<Customer>(), x=>x.Id);

        IRepository<CustomerStore, long> IPurchasingUnitOfWork.CustomerStores => GetRepository(x => x.Set<CustomerStore>(), x=>x.Id);

        IRepository<Crest, long> IPurchasingUnitOfWork.Crests => GetRepository(x => x.Set<Crest>(), x=>x.Id);

        IRepository<Order, long> IPurchasingUnitOfWork.Orders => GetRepository(x => x.Set<Order>(), x=>x.Id);

        IRepository<Employee, long> IPurchasingUnitOfWork.Employees => GetRepository(x => x.Set<Employee>(), x=>x.Id);

        IRepository<EmployeeTask, long> IPurchasingUnitOfWork.Tasks => GetRepository(x => x.Set<EmployeeTask>(), x=>x.Id);

        IRepository<Evaluation, long> IPurchasingUnitOfWork.Evaluations => GetRepository(x => x.Set<Evaluation>(), x=>x.Id);

        IRepository<Picture, long> IPurchasingUnitOfWork.Pictures => GetRepository(x => x.Set<Picture>(), x=>x.Id);

        IRepository<Probation, long> IPurchasingUnitOfWork.Probations => GetRepository(x => x.Set<Probation>(), x=>x.Id);

        IRepository<OrderItem, long> IPurchasingUnitOfWork.OrderItems => GetRepository(x => x.Set<OrderItem>(), x=>x.Id);

        IRepository<Product, long> IPurchasingUnitOfWork.Products => GetRepository(x => x.Set<Product>(), x=>x.Id);

        IRepository<ProductCatalog, long> IPurchasingUnitOfWork.ProductCatalogs => GetRepository(x => x.Set<ProductCatalog>(), x=>x.Id);

        IRepository<ProductImage, long> IPurchasingUnitOfWork.ProductImages => GetRepository(x => x.Set<ProductImage>(), x=>x.Id);

        IRepository<Quote, long> IPurchasingUnitOfWork.Quotes => GetRepository(x => x.Set<Quote>(), x=>x.Id);

        IRepository<QuoteItem, long> IPurchasingUnitOfWork.QuoteItems => GetRepository(x => x.Set<QuoteItem>(), x=>x.Id);

        IRepository<State, StateEnum> IPurchasingUnitOfWork.States => GetRepository(x => x.Set<State>(), x=>x.ShortName);

        IRepository<DatabaseVersion, long> IPurchasingUnitOfWork.Version => GetRepository(x => x.Set<DatabaseVersion>(), x=>x.Id);
    }
}
