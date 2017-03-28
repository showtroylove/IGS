using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;

namespace DevExpress.DevAV.ViewModels {
    public class PurchasingViewModel : DocumentsViewModel<PurchasingModuleDescription, IPurchasingUnitOfWork> 
    {
        private const string PlanningGroup = "Planning";
        private const string OperationsGroup = "Scheduling";
        private const string SystemGroup = "Reference Info";

        public PurchasingViewModel(): base(UnitOfWorkSource.GetUnitOfWorkFactory()) 
        {
            IsTablet = false;
        }

        protected override PurchasingModuleDescription[] CreateModules() 
        {
            var modules = new[] 
            {
                new PurchasingModuleDescription("Dashboard", "DashboardView", PlanningGroup, FiltersSettings.GetDashboardFilterTree(this)),
                new PurchasingModuleDescription("Tasks", "TaskCollectionView", PlanningGroup, FiltersSettings.GetTasksFilterTree(this)),
                new PurchasingModuleDescription("Setup", "ProductCollectionView", OperationsGroup, FiltersSettings.GetProductsFilterTree(this), "Products"),
                new PurchasingModuleDescription("Supply", "OrderCollectionView", OperationsGroup, FiltersSettings.GetSalesFilterTree(this), "Sales"),
                new PurchasingModuleDescription("Companies", "CustomerCollectionView", SystemGroup, FiltersSettings.GetCustomersFilterTree(this), "Customers"),
                new PurchasingModuleDescription("Team", "EmployeeCollectionView", PlanningGroup, FiltersSettings.GetEmployeesFilterTree(this), "Employees"),
                new PurchasingModuleDescription("Settings", "AppSettingsCollectionMainView", SystemGroup, FiltersSettings.GetAppSettingsDetailFilterTree(this), "Automation")
            };

            foreach(var module in modules) 
            {
                var moduleRef = module;
                module.FilterTreeViewModel.NavigateAction = () => Show(moduleRef);
            }
            return modules;
        }

        protected override void OnActiveModuleChanged(PurchasingModuleDescription oldModule) 
        {
            base.OnActiveModuleChanged(oldModule);
            ActiveModule?.FilterTreeViewModel?.SetViewModel(DocumentManagerService.ActiveDocument.Content);
        }

        protected override string GetModuleTitle(PurchasingModuleDescription module) => $"{base.GetModuleTitle(module)} - Puchasing";

        public override void OnLoaded() 
        {
            base.OnLoaded();
            IsTablet = DeviceDetector.IsTablet;
        }
        
        protected override PurchasingModuleDescription DefaultModule => Modules[Modules.Length-1];

        public virtual bool IsTablet { get; set; }
    }
}
