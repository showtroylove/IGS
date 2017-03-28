using System.Windows.Input;
using DevExpress.Mvvm;

namespace DevExpress.DevAV.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand _navigateCommand;
        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
        public void Navigate(string target) => NavigationService.Navigate(target, null, this);

        private INavigationService NavigationService => GetService<INavigationService>();
        public ICommand OnViewLoadedCommand => new DelegateCommand(OnViewLoaded);
        
        public void OnViewLoaded() 
        {
            Navigate("HomeView");
        }
    }
}
