using System.Windows;
using System.Windows.Data;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Navigation;

namespace DevExpress.DevAV.Common.View {
    public class TileBarFocusBehavior : Behavior<TileBar> 
    {
        private static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(PurchasingModuleDescription), typeof(TileBarFocusBehavior),
            new FrameworkPropertyMetadata(null, (d, e) => ((TileBarFocusBehavior)d).OnSelectedItemChanged(e)));
        protected override void OnAttached() {
            base.OnAttached();
            BindingOperations.SetBinding(this, SelectedItemProperty, new Binding("SelectedItem") { Source = AssociatedObject, Mode = BindingMode.OneWay });
        }
        protected override void OnDetaching() {
            BindingOperations.ClearBinding(this, SelectedItemProperty);
            base.OnDetaching();
        }

        private void OnSelectedItemChanged(DependencyPropertyChangedEventArgs e) {
            var uiElement = e.NewValue == null ? null : (UIElement)AssociatedObject.ItemContainerGenerator.ContainerFromItem(e.NewValue);
            uiElement?.Focus();
        }
    }
}
