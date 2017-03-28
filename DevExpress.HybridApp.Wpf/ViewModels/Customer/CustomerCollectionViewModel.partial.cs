using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevExpress.Mvvm;

namespace DevExpress.DevAV.ViewModels {
    partial class CustomerCollectionViewModel : ISupportFiltering<Customer> {
        protected override void OnEntitiesLoaded(DevAVDbDataModel.IPurchasingUnitOfWork unitOfWork, IEnumerable<CustomerInfoWithSales> entities) {
            base.OnEntitiesLoaded(unitOfWork, entities);
            QueriesHelper.UpdateCustomerInfoWithSales(entities, unitOfWork.CustomerStores, unitOfWork.CustomerEmployees, unitOfWork.Orders.ActualOrders());
        }
        public virtual FilterTreeViewModel<Customer, long> FilterTreeViewModel { get; set; }
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Customer>());
        }
        #region ISupportFiltering
        Expression<Func<Customer, bool>> ISupportFiltering<Customer>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}
