using System.Windows;
using DevExpress.Xpf.Core;

namespace DevExpress.DevAV {
    public partial class SplashScreenWindow : Window, ISplashScreen {
        public SplashScreenWindow() {
            Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Hidden : Visibility.Visible;
            InitializeComponent();
        }

        void ISplashScreen.CloseSplashScreen() {
            Close();
        }
        void ISplashScreen.Progress(double value) {
            progressBar.Value = value;
        }
        void ISplashScreen.SetProgressState(bool isIndeterminate) {
        }
    }
}
