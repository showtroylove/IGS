using System.Windows;
using System.Windows.Data;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Navigation;

namespace DevExpress.DevAV {
    public class FilterUnselectionBehavior : Behavior<TileBar> {
        private bool selectFilterEnable = true;

        public static readonly DependencyProperty SelectedFilterProperty =
            DependencyProperty.Register("SelectedFilter", typeof(FilterItem), typeof(FilterUnselectionBehavior),
                new PropertyMetadata(null, (d, e) => ((FilterUnselectionBehavior)d).OnSelectedFilterChanged()));

        private static readonly DependencyProperty TileBarItemInternalProperty =
            DependencyProperty.Register("TilebarItemInternal", typeof(FilterItem), typeof(FilterUnselectionBehavior),
                new PropertyMetadata(null, (d, e) => ((FilterUnselectionBehavior)d).OnTileBarItemInternalChanged()));

        public FilterItem SelectedFilter {
            get { return (FilterItem)GetValue(SelectedFilterProperty); }
            set { SetValue(SelectedFilterProperty, value); }
        }

        private FilterItem TileBarItemInternal {
            get { return (FilterItem)GetValue(TileBarItemInternalProperty); }
            set { SetValue(TileBarItemInternalProperty, value); }
        }

        private void OnSelectedFilterChanged() {
            if(AssociatedObject == null || AssociatedObject.ItemsSource == null || SelectedFilter == TileBarItemInternal) return;
            if(SelectedFilter == null) {
                SelectTileBarItem(null);
                return;
            }
            foreach(var item in AssociatedObject.ItemsSource)
                if(item == SelectedFilter) {
                    SelectTileBarItem(SelectedFilter);
                    return;
                }
            SelectTileBarItem(null);
        }

        private void OnTileBarItemInternalChanged() {
            if(selectFilterEnable)
                SelectedFilter = TileBarItemInternal;
        }

        protected override void OnAttached() {
            base.OnAttached();
            BindingOperations.SetBinding(this, TileBarItemInternalProperty, new Binding("SelectedItem") { Source = AssociatedObject, Mode = BindingMode.OneWay });
            OnSelectedFilterChanged();
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            BindingOperations.ClearBinding(this, TileBarItemInternalProperty);
        }

        private void SelectTileBarItem(FilterItem item) {
            selectFilterEnable = false;
            AssociatedObject.SelectedItem = item;
            selectFilterEnable = true;
        }
    }
}
