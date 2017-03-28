using System.Windows.Controls;
using System.Windows;
using DevExpress.Xpf.Charts;

namespace DevExpress.DevAV.Views {
    public partial class DashboardView : UserControl {
        private const int highTopSpacing = 10;
        private const int lowTopSpacing = -15;
        private const int highBottomSpacing = 5;
        private const int lowBottomSpacing = 0;
        private const int heightThreshold = 150;

        public DashboardView() {
            InitializeComponent();
        }

        private void goodsSold_SizeChanged(object sender, SizeChangedEventArgs e) {
            var legend = ((ChartControl)sender).Legend;
            if(e.NewSize.Height < heightThreshold && ((int)legend.Margin.Top != lowTopSpacing || (int)legend.Margin.Bottom != lowBottomSpacing))
                legend.Margin = new Thickness { Top = lowTopSpacing, Bottom = lowBottomSpacing };
            if(e.NewSize.Height >= heightThreshold && ((int)legend.Margin.Top != highTopSpacing || (int)legend.Margin.Bottom != highBottomSpacing))
                legend.Margin = new Thickness { Top = highTopSpacing, Bottom = highBottomSpacing };
        }
    }
}
