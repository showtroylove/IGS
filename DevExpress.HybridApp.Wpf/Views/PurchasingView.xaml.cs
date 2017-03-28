using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class PurchasingView : UserControl {
        public PurchasingView() {
            InitializeComponent();
        }

        private void OnNavButtonCloseClick(object sender, EventArgs e) {
            Application.Current.MainWindow.Close();
        }
    }
}
