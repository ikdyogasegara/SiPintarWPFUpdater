using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Main.Web;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Main
{
    public class WebViewModel : ViewModelBase
    {
        private readonly string _configPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenWeblinkCommand { get; }
        public ICommand OnRetryErrorCommand { get => OnLoadCommand; }

        public WebViewModel(MainViewModel parent, IRestApiClientModel restApi)
        {
            _ = parent;
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenWeblinkCommand = new OnOpenWeblinkCommand(this, parent);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                OnPropertyChanged();
            }
        }

        private List<List<object>> _listModule;
        public List<List<object>> ListModule
        {
            get => _listModule;
            set
            {
                _listModule = value;
                OnPropertyChanged();
            }
        }

        public List<object> ModuleItems()
        {
            DotNetEnv.Env.Load(_configPath);
            var dashboardPath = Application.Current.Resources["dashboard_url"].ToString();
            var pdamInfoPath = Application.Current.Resources["pdam_info_url"].ToString();

            return new List<object>()
            {
                new {
                    Key = "dashboard",
                    Title = "Dashboard",
                    Logo = "/SiPintar;component/Assets/Images/Application/dashboard_logo_3x.png",
                    Description = "Aplikasi dashboard yang dibuat untuk manajemen data yang digunakan di PDAM Pintar.",
                    Type = "website",
                    Url = dashboardPath
                },
                new {
                    Key = "pdam_info",
                    Title = "PDAM Info",
                    Logo = "/SiPintar;component/Assets/Images/Application/pdam_info_logo_3x.png",
                    Description = "Website yang berisikan informasi seputar PDAM Pintar dan lain-lain.",
                    Type = "website",
                    Url = pdamInfoPath
                },
            };
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
