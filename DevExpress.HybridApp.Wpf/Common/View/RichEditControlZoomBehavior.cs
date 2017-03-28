using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.RichEdit;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace DevExpress.DevAV {
    public class RichEditControlZoomBehavior : Behavior<RichEditControl> {
        private static float minZoomFactor = 0.3f;
        private static float maxZoomFactor = 1.7f;
        private static float stepZoomFactor = 0.1f;

        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }

        public RichEditControlZoomBehavior() {
            ZoomInCommand = new DelegateCommand(
                () => AssociatedObject.ActiveView.ZoomFactor += stepZoomFactor,
                () => AssociatedObject != null && AssociatedObject.ActiveView != null && AssociatedObject.ActiveView.ZoomFactor + stepZoomFactor < maxZoomFactor);
            ZoomOutCommand = new DelegateCommand(
                () => AssociatedObject.ActiveView.ZoomFactor -= stepZoomFactor,
                () => AssociatedObject != null && AssociatedObject.ActiveView != null && AssociatedObject.ActiveView.ZoomFactor - stepZoomFactor > minZoomFactor);
        }
    }
}
