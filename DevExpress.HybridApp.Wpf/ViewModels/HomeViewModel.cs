using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Properties;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevExpress.DevAV.ViewModels
{
    public sealed class HomeViewModel : ViewModelBase
    {
        private IEnumerable<HomeWithPhoto> _homeRepositoryTileDataSource;
        private HomeWithPhoto _homeRepositoryTileData;
        private HomeWithPhoto _nextHomeRepositoryTileData;
        private bool _animateHomeRepositoryTileContent;
        private IEnumerator<HomeWithPhoto> _homeRepositoryTileDataEnumerator;
        private IEnumerable<Agent> _agentRepositoryTileDataSource;
        private Agent _agentRepositoryTileData;
        private Agent _nextAgentRepositoryTileData;
        private bool _animateAgentRepositoryTileContent;
        private IEnumerator<Agent> _agentRepositoryTileDataEnumerator;

        private ICommand _navigateCommand;
        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
        
        public void Navigate(string target) => NavigationService.Navigate(target, null, this);

        private INavigationService NavigationService => GetService<INavigationService>();
        public ICommand OnViewLoadedCommand => new DelegateCommand(OnViewLoaded);

        public HomeViewModel()
        {
            Refresh();
        }

        public void OnViewLoaded() => Navigate("HomeView");

        public IEnumerable<HomeWithPhoto> HomeRepositoryTileDataSource 
        {
            get { return _homeRepositoryTileDataSource; }
            private set { SetProperty(ref _homeRepositoryTileDataSource, value, () => HomeRepositoryTileDataSource); }
        }
        public IEnumerable<Agent> AgentRepositoryTileDataSource 
        {
            get { return _agentRepositoryTileDataSource; }
            private set { SetProperty(ref _agentRepositoryTileDataSource, value, () => AgentRepositoryTileDataSource); }
        }
        public bool AnimateHomeRepositoryTileContent 
        {
            get { return _animateHomeRepositoryTileContent; }
            private set { SetProperty(ref _animateHomeRepositoryTileContent, value, () => AnimateHomeRepositoryTileContent); }
        }
        public bool AnimateAgentRepositoryTileContent 
        {
            get { return _animateAgentRepositoryTileContent; }
            private set { SetProperty(ref _animateAgentRepositoryTileContent, value, () => AnimateAgentRepositoryTileContent); }
        }

        private void Refresh() 
        {
            LoadNextHomeRepositoryTileData();
            LoadNextAgentRepositoryTileData();
        }

        private IEnumerable<Agent> AgentRepositoryTileDataSourceCore 
        {
            get 
            {
                while(true) 
                {
                    if(_nextAgentRepositoryTileData != null)
                    {
                        _agentRepositoryTileData = _nextAgentRepositoryTileData;
                        LoadNextAgentRepositoryTileData();
                    }
                    yield return _agentRepositoryTileData;
                }
            }
        }
        
        private IEnumerable<HomeWithPhoto> HomeRepositoryTileDataSourceCore 
        {
            get
            {
                while(true)
                {
                    if(_nextHomeRepositoryTileData != null) 
                    {
                        _homeRepositoryTileData = _nextHomeRepositoryTileData;
                        LoadNextHomeRepositoryTileData();
                    }
                    yield return _homeRepositoryTileData;
                }
            }
        }

        private void LoadNextHomeRepositoryTileData() 
        {
            _nextHomeRepositoryTileData = null;
            Task.Factory.StartNew(() => 
            {
                if(_homeRepositoryTileDataEnumerator == null || !_homeRepositoryTileDataEnumerator.MoveNext()) 
                {
                    _homeRepositoryTileDataEnumerator = GetHomeRepositoryTileDataEnumerator();
                    if(!_homeRepositoryTileDataEnumerator.MoveNext()) return;
                }

                _nextHomeRepositoryTileData = _homeRepositoryTileDataEnumerator.Current;

                if (HomeRepositoryTileDataSource != null) return;

                HomeRepositoryTileDataSource = HomeRepositoryTileDataSourceCore;
                AnimateHomeRepositoryTileContent = true;
            });
        }

        private void LoadNextAgentRepositoryTileData() 
        {
            _nextAgentRepositoryTileData = null;
            Task.Factory.StartNew(() => 
            {
                if(_agentRepositoryTileDataEnumerator == null || !_agentRepositoryTileDataEnumerator.MoveNext()) 
                {
                    _agentRepositoryTileDataEnumerator = GetAgentRepositoryTileDataEnumerator();
                    if(!_agentRepositoryTileDataEnumerator.MoveNext()) return;
                }
                _nextAgentRepositoryTileData = _agentRepositoryTileDataEnumerator.Current;

                if (AgentRepositoryTileDataSource != null) return;

                AgentRepositoryTileDataSource = AgentRepositoryTileDataSourceCore;
                AnimateAgentRepositoryTileContent = true;
            });
        }

        private static IEnumerator<HomeWithPhoto> GetHomeRepositoryTileDataEnumerator() 
        {
            yield return new HomeWithPhoto { Home = new Home {Address = "Transco - Williams"}, Image = Resources._001_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Algonquinn"}, Image = Resources._002_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Texas Eastern"}, Image = Resources._003_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Big Sandy"}, Image = Resources._004_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Dominion"}, Image = Resources._005_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Iroquois"}, Image = Resources._006_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "National Fuel"}, Image = Resources._007_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Stage Coach"}, Image = Resources._008_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Tennessee"}, Image = Resources._009_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Columbia Gas"}, Image = Resources._010_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "TransCanada"}, Image = Resources._011_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Honeoye"}, Image = Resources._012_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Troy Stevens NG"}, Image = Resources._013_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Steckman Ridge"}, Image = Resources._014_pipe.ToByteArray(ImageFormat.Bmp) };
            yield return new HomeWithPhoto { Home = new Home {Address = "Levine Pipeline"}, Image = Resources._015_pipe.ToByteArray(ImageFormat.Bmp) };
        }

        private static IEnumerator<Agent> GetAgentRepositoryTileDataEnumerator()
        {
            yield return
                new Agent
                {
                    ID = 1,
                    Photo = Resources.AgentPhoto.ToByteArray(ImageFormat.Png),
                    FirstName = "Walled",
                    LastName = "Wally",
                    Email = "walled_wally@coned.com",
                    Phone = "(212) 460-2078"
                };
        }
    }
}
