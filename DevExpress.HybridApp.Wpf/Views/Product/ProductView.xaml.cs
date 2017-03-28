using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class ProductView : UserControl {
        public ProductView() {
            InitializeComponent();
        }

        private void PdfViewerControl_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e) {
            e.Handled = true;
        }
    }
}
