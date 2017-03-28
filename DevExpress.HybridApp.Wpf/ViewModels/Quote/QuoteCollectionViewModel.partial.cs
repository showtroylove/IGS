using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.DataModel;


namespace DevExpress.DevAV.ViewModels {
    partial class QuoteCollectionViewModel {
        private const int NumberOfAverageQuotes = 300;

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            UpdateAverageQuotes();
        }
        public virtual List<Quote> AverageQuotes { get; protected set; }
        public virtual object SelectedMapItem { get; set; }

        public IList<QuoteSummaryItem> GetOpportunitiesInfo(DateTime start, DateTime end) {
            return QueriesHelper.GetSummaryOpportunities(CreateReadOnlyRepository().GetFilteredEntities(null).Where(x => x.Date >= start && x.Date <= end)).ToList();
        }

        protected override void OnIsLoadingChanged() {
            base.OnIsLoadingChanged();
            if(!IsLoading) {
                UpdateAverageQuotes();
            }
        }

        private void UpdateAverageQuotes() {
            AverageQuotes = QueriesHelper.GetAverageQuotes(CreateReadOnlyRepository().GetFilteredEntities(null), NumberOfAverageQuotes);
        }

        public IEnumerable<object> UpdateMapItems(DateTime start, DateTime end) {
            IEnumerable<object> mapItems;
            var items = GetOpportunities(Stage.Summary, x => x.Date > start && x.Date < end).GroupBy(item => item.Address, new AddressComparer());

            if(items.Count() > 0) {
                var minValue = items.Min(group => group.CustomSum(quote => quote.Value));
                var maxValue = items.Max(group => group.CustomSum(quote => quote.Value));
                var dif = (maxValue - minValue > (decimal)0.01) ? (maxValue - minValue) : 1;
                mapItems = items.Select(group => new {
                    Address = group.Key,
                    Total = group.CustomSum(item => item.Value),
                    AbsSize = (double)((group.CustomSum(item => item.Value) - minValue) / dif),
                });
            } else
                mapItems = Enumerable.Empty<object>();
            SelectedMapItem = mapItems.LastOrDefault();
            return mapItems;
        }

        private IList<QuoteMapItem> GetOpportunities(Stage stage, Expression<Func<Quote, bool>> filterExpression = null) {
            var unitOfWork = unitOfWorkFactory.CreateUnitOfWork();
            var quotes = unitOfWork.Quotes.GetFilteredEntities(filterExpression).ActualQuotes();
            return QueriesHelper.GetOpportunities(quotes, unitOfWork.Customers, stage).ToList();
        }
    }

    internal class AddressComparer : IEqualityComparer<Address> {
        public bool Equals(Address x, Address y) {
            return x.State.Equals(y.State) && x.City.Equals(y.City);
        }
        public int GetHashCode(Address obj) {
            return obj.State.GetHashCode() ^ obj.City.GetHashCode();
        }
    }
}
