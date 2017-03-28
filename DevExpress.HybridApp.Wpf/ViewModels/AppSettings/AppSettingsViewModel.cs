using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.LayoutControl;
using IGS.Data.Model;

namespace DevExpress.DevAV.ViewModels.AppSettings
{
    /// <summary>
    /// Represents the Products collection view model.
    /// </summary>
    public partial class AppSettingsViewModel : ViewModelBase
    {
        public AppSettingsViewModel()
        {
            //var result = WebApiUnitOfWork<IGS.Data.Model.AppSettings>.GetAppSettings();
            //AppSettings = new ObservableCollection<IGS.Data.Model.AppSettings>(result);
            //_currentItem = AppSettings.FirstOrDefault();
            //AppSettingsTypes = Enum.GetNames(typeof(AppSettingsTypeEnum)).ToList();
        }

        private ICommand _navigateCommand;
        private IGS.Data.Model.AppSettings _currentItem  = null;
        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
        public ObservableCollection<IGS.Data.Model.AppSettings> AppSettings  {get;}

        public List<string> AppSettingsTypes {get;}

        public List<string> Environments => new List<string> {"Production", "Test", "Development"};

        public IGS.Data.Model.AppSettings CurrentItem
        {
            get { return _currentItem; }
            set { SetProperty(() => CurrentItem, value);  }
        }

        public ObservableCollection<AppSettingsDetails> AppSettingsDetails 
            => new ObservableCollection<AppSettingsDetails>(CurrentItem.Settings);

        public void Navigate(string target) => NavigationService.Navigate(target, null, this);

        private INavigationService NavigationService => GetService<INavigationService>();
        public bool IsLoading => false;
        
        [Command]
        public void Save() 
        {
            //... 
        }
        public bool CanSave() => true;

        [Command]
        public void Edit() 
        {
            //... 
        }
        public bool CanEdit() => true;

        [Command]
        public void New() 
        {
            //... 
        }
        public bool CanNew() => true;

        public bool CanCreateCustomFilter() => false;

        [Command]
        public void GroupBox_MouseLeftButtonUp() => this.GroupState = GroupState == GroupBoxState.Normal ? GroupBoxState.Maximized : GroupBoxState.Normal;

        public bool CanGroupBox_MouseLeftButtonUp() => true;

        public GroupBoxState GroupState {get; set;}
    }
}
