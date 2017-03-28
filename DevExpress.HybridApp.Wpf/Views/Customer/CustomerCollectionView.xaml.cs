using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Reflection;

namespace DevExpress.DevAV.Views {
    public partial class CustomerCollectionView : UserControl {
        public CustomerCollectionView() {
            InitializeComponent();
        }
    }
    public class SlideViewTemplateSelector : DataTemplateSelector {
        public DataTemplate ContactsTemplate { get; set; }
        public DataTemplate StoresTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is CustomerEmployee)
                return ContactsTemplate;
            if(item is CustomerStore)
                return StoresTemplate;
            return base.SelectTemplate(item, container);
        }
    }
}
namespace DevExpress.DevAV.Common.View {
    public class DpiResizingPanel : ContentControl {
        private const double defaultDpi = 96d;
        public DpiResizingPanel() {
            ResizeByDpi();
        }

        private static double GetDpiXFactor() { return GetDpiFactor("DpiX"); }
        private static double GetDpiYFactor() { return GetDpiFactor("Dpi"); }

        private static double GetDpiFactor(string propName) {
            var dpiProperty = typeof(SystemParameters).GetProperty(propName, BindingFlags.NonPublic | BindingFlags.Static);
            var dpi = (int)dpiProperty.GetValue(null, null);
            return dpi / defaultDpi;
        }

        private static double CorrectDpiFactor(double factor) {
            return factor > 1.5 ? 1.5 : factor;
        }

        private void ResizeByDpi() {
            if(SystemParameters.PrimaryScreenHeight > 1500 && SystemParameters.PrimaryScreenWidth > 2000)
                return;
            var dpiXFactor = CorrectDpiFactor(GetDpiXFactor());
            var dpiYFactor = CorrectDpiFactor(GetDpiYFactor());
            LayoutTransform = new ScaleTransform(1 / dpiXFactor, 1 / dpiYFactor);
            float touchScaleFactor, fontSize;
            Utils.DeviceDetector.SuggestHybridDemoParameters(out touchScaleFactor, out fontSize);
            FontSize = 12 * dpiXFactor;
        }
    }
}
