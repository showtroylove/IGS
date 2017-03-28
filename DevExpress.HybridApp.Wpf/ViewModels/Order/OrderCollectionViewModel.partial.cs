using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    partial class OrderCollectionViewModel {
        private const int NumberOfAverageOrders = 200;
        public virtual List<Order> AverageOrders { get; set; }
        public virtual List<SalesInfo> Sales { get; set; }
        public virtual SalesInfo SelectedSale { get; set; }

        public void ShowPrintPreview() {
            var link = this.GetRequiredService<Common.View.IPrintableControlPreviewService>().GetLink();
            this.GetRequiredService<IDocumentManagerService>().CreateDocument("PrintableControlPrintPreview", PrintableControlPreviewViewModel.Create(link), null, this).Show();
        }

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            var unitOfWork = unitOfWorkFactory.CreateUnitOfWork();
            Sales = QueriesHelper.GetSales(unitOfWork.OrderItems);
            SelectedSale = Sales[0];
            AverageOrders = QueriesHelper.GetAverageOrders(unitOfWork.Orders.ActualOrders(), NumberOfAverageOrders);
        }
    }
}
