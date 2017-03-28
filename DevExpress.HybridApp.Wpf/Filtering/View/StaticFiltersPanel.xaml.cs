using System.Windows;
using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class StaticFiltersPanel : UserControl {
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(StaticFiltersPanel), new PropertyMetadata(null));

        public StaticFiltersPanel() {
            InitializeComponent();
        }
    }
}
