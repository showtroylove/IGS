using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class CustomFilterView : UserControl {
        private CustomFilterViewModel ViewModel { get { return (CustomFilterViewModel)DataContext; } }
        public CustomFilterView() {
            InitializeComponent();
        }
    }
}
