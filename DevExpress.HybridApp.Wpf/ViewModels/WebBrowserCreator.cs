using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace DevExpress.DevAV.ViewModels {
    public class WebBrowserCreator : Grid {

        #region Dependency Properties
        public static readonly DependencyProperty IsLoadingProperty;
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty ShowBrowserProperty;
        static WebBrowserCreator() {
            var ownerType = typeof(WebBrowserCreator);
            IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), ownerType, new PropertyMetadata(null, RaiseSourceChanged));
            SourceProperty = DependencyProperty.Register("Source", typeof(Uri), ownerType, new PropertyMetadata(null, RaiseSourceChanged));
            ShowBrowserProperty = DependencyProperty.Register("ShowBrowser", typeof(bool), ownerType, new PropertyMetadata(false, RaiseShowBrowserChanged));
        }

        private static void RaiseSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((WebBrowserCreator)d).RaiseSourceChanged(e);
        }

        private static void RaiseShowBrowserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((WebBrowserCreator)d).RaiseShowBrowserChanged(e);
        }
        #endregion

        private WebBrowser _browser;

        public WebBrowserCreator() 
        {
            Background = new SolidColorBrush(Colors.White);
        }

        public bool IsLoading { get { return (bool)GetValue(IsLoadingProperty); } set { SetValue(IsLoadingProperty, value); } }
        public Uri Source { get { return (Uri)GetValue(SourceProperty); } set { SetValue(SourceProperty, value); } }
        public bool ShowBrowser { get { return (bool)GetValue(ShowBrowserProperty); } set { SetValue(ShowBrowserProperty, value); } }

        private void DoShowBrowser() 
        {
            _browser = new WebBrowser {Visibility = Visibility.Collapsed};
            Children.Add(_browser);
            IsLoading = true;
            _browser.LoadCompleted += OnBrowserLoadCompleted;
            UpdateBrowserSource();
        }

        private void DoHideBrowser() 
        {
            Children.Remove(_browser);
            _browser = null;
        }

        private void OnBrowserLoadCompleted(object sender, NavigationEventArgs e) 
        {
            if(_browser == null) return;
            _browser.LoadCompleted -= OnBrowserLoadCompleted;
            _browser.Visibility = Visibility.Visible;
            IsLoading = false;
        }

        private void RaiseSourceChanged(DependencyPropertyChangedEventArgs e) 
        {
            UpdateBrowserSource();
        }

        private void UpdateBrowserSource() 
        {
            if(_browser != null)
                _browser.Source = Source;
        }

        private void RaiseShowBrowserChanged(DependencyPropertyChangedEventArgs e) 
        {
            var newValue = (bool)e.NewValue;
            if(newValue)
                DoShowBrowser();
            else
                DoHideBrowser();
        }
    }
}
