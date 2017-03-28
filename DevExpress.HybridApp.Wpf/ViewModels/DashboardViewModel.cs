using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DevExpress.DevAV.DevAVDbDataModel;
using System;

namespace DevExpress.DevAV.ViewModels {
    public partial class DashboardViewModel {
        private IPurchasingUnitOfWork unitOfWork = UnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();

        public DashboardViewModel() {
            DashboardInitialization();
        }
        public virtual IList<OrderInfo> DashboardOrders { get; set; }
        public virtual IList<QuoteSummaryItem> SummaryOpportunities { get; set; }
        public virtual IList<SalesSummaryItem> SalesSummarySelectedItem { get; set; }
        public virtual IList<CostAverageItem> CostSelectedItem { get; set; }
        public virtual ObservableCollection<bool> GoodSoldPeriodSelector { get; set; }
        public virtual ObservableCollection<bool> RevenuePeriodSelector { get; set; }

        private List<IEnumerable<SalesSummaryItem>> salesSummaryItems;
        private List<IEnumerable<CostAverageItem>> costAverageItems;

        private void GoodSoldSelectorChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if((bool)e.NewItems[0])
                CostSelectedItem = costAverageItems[e.NewStartingIndex].ToList();
            SelectorReset(GoodSoldPeriodSelector, e);
        }

        private void RevenuesSelectorChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if((bool)e.NewItems[0])
                SalesSummarySelectedItem = salesSummaryItems[e.NewStartingIndex].ToList();
            SelectorReset(RevenuePeriodSelector, e);
        }

        private List<IEnumerable<SalesSummaryItem>> GetSalesSummaryItems() {
            return new List<IEnumerable<SalesSummaryItem>>
            {
            QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.ThisYear),
            QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.ThisMonth),
            QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.FixedDate, LastOrderDate(unitOfWork))
            };
        }

        private List<IEnumerable<CostAverageItem>> GetCostAverageItems() {
            return new List<IEnumerable<CostAverageItem>>
            {
            QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.ThisYear),
            QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.ThisMonth),
            QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.FixedDate, LastOrderDate(unitOfWork))
            };
        }

        private static DateTime LastOrderDate(IPurchasingUnitOfWork unitOfWork) {
            return unitOfWork.OrderItems.Where(x => x.Order.OrderDate <= DateTime.Today).Max(x => x.Order.OrderDate);
        }

        private void DashboardInitialization() {
            SummaryOpportunities = QueriesHelper.GetSummaryOpportunities(unitOfWork.Quotes).ToList();
            GoodSoldPeriodSelector = new ObservableCollection<bool> { true, false, false };
            RevenuePeriodSelector = new ObservableCollection<bool> { true, false, false };
            GoodSoldPeriodSelector.CollectionChanged += GoodSoldSelectorChanged;
            RevenuePeriodSelector.CollectionChanged += RevenuesSelectorChanged;
            salesSummaryItems = GetSalesSummaryItems();
            costAverageItems = GetCostAverageItems();
            DashboardOrders = QueriesHelper.GetOrderInfo(unitOfWork.Orders);
            SalesSummarySelectedItem = salesSummaryItems[0].ToList();
            CostSelectedItem = costAverageItems[0].ToList();
        }

        private void SelectorReset(ObservableCollection<bool> collection, NotifyCollectionChangedEventArgs e) {
            if((bool)e.NewItems[0]) {
                for(var i = 0; i < collection.Count; i++)
                    if(i != e.NewStartingIndex && collection[i])
                        collection[i] = false;
            } else
                if(!collection.Contains(true)) collection[e.NewStartingIndex] = true;
        }
    }
}
