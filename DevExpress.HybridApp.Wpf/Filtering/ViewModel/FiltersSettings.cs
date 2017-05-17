using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Properties;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;


namespace DevExpress.DevAV.ViewModels {
    internal static class FiltersSettings {

        public static FilterTreeViewModel<IGS.Data.Model.AppSettings, int> GetAppSettingsDetailFilterTree(object parentViewModel) => 
            FilterTreeViewModel<IGS.Data.Model.AppSettings, int>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, null, null, null, null),
            CreateAppSettingsUnitOfWork().AppSettings, RegisterEntityChangedMessageHandler<IGS.Data.Model.AppSettings, int>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Employee, long> GetDashboardFilterTree(object parentViewModel) => FilterTreeViewModel<Employee, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, null, null, null, null),
            CreateUnitOfWork().Employees, RegisterEntityChangedMessageHandler<Employee, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<EmployeeTask, long> GetTasksFilterTree(object parentViewModel) => FilterTreeViewModel<EmployeeTask, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, null, x => x.TasksStaticFilters, null, null),
            CreateUnitOfWork().Tasks, RegisterEntityChangedMessageHandler<EmployeeTask, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Employee, long> GetEmployeesFilterTree(object parentViewModel) => FilterTreeViewModel<Employee, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, "Status", x => x.EmployeesStaticFilters, null, null),
            CreateUnitOfWork().Employees, RegisterEntityChangedMessageHandler<Employee, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Product, long> GetProductsFilterTree(object parentViewModel) => FilterTreeViewModel<Product, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, "Category", x => x.ProductsStaticFilters, x => x.ProductsCustomFilters, null,
                new[] {
                    BindableBase.GetPropertyName(() => new Product().Id),
                    BindableBase.GetPropertyName(() => new Product().EngineerId),
                    BindableBase.GetPropertyName(() => new Product().PrimaryImageId),
                    BindableBase.GetPropertyName(() => new Product().SupportId),
                    BindableBase.GetPropertyName(() => new Product().Support),
                }),
            CreateUnitOfWork().Products, RegisterEntityChangedMessageHandler<Product, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Customer, long> GetCustomersFilterTree(object parentViewModel) => FilterTreeViewModel<Customer, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, "Favorites", null, x => x.CustomersCustomFilters,
                new[] {
                    BindableBase.GetPropertyName(() => new Customer().Id),
                },
                new[] {
                    BindableBase.GetPropertyName(() => new Customer().BillingAddress) + "." + BindableBase.GetPropertyName(() => new Address().City),
                    BindableBase.GetPropertyName(() => new Customer().BillingAddress) + "." + BindableBase.GetPropertyName(() => new Address().State),
                    BindableBase.GetPropertyName(() => new Customer().BillingAddress) + "." + BindableBase.GetPropertyName(() => new Address().ZipCode),
                }),
            CreateUnitOfWork().Customers, RegisterEntityChangedMessageHandler<Customer, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Order, long> GetSalesFilterTree(object parentViewModel) => FilterTreeViewModel<Order, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, null, null, null, null),
            CreateUnitOfWork().Orders.ActualOrders(), RegisterEntityChangedMessageHandler<Order, long>
            ).SetParentViewModel(parentViewModel);

        public static FilterTreeViewModel<Quote, long> GetOpportunitiesFilterTree(object parentViewModel) => FilterTreeViewModel<Quote, long>.Create(
            new FilterTreeModelPageSpecificSettings<Settings>(Settings.Default, null, null, null, null),
            CreateUnitOfWork().Quotes.ActualQuotes(), RegisterEntityChangedMessageHandler<Quote, long>
            ).SetParentViewModel(parentViewModel);

        private static IPurchasingUnitOfWork CreateUnitOfWork() => UnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
        private static IAppSettingsUnitOfWork CreateAppSettingsUnitOfWork() => UnitOfWorkSource.GetWebApiUnitOfWorkFactory().CreateUnitOfWork();

        private static void RegisterEntityChangedMessageHandler<TEntity, TPrimaryKey>(object recipient, Action handler) => Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(recipient, message => handler());
    }
}
