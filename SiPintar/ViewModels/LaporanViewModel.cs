using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Authentication;
using SiPintar.Commands.Global.BackgroundProcess;
using SiPintar.Commands.Laporan.Navigator;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.ViewModels.Laporan.Factories;


namespace SiPintar.ViewModels
{
    public class LaporanViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        public ViewModelBase CurrentViewModel => _navigator?.LaporanCurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand OnLogoutCommand { get; }
        public ICommand OnOpenLogoutDialogCommand { get; }
        public ICommand OnBackToMainMenuCommand { get; }
        public ICommand OnOpenListDialogCommand { get; }
        public ICommand OnAddToBgProcessCommand { get; }
        public ICommand OnRefreshListDialogCommand { get; }
        public ICommand OnClearListDialogCommand { get; }

        public LaporanViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IRestApiClientModel restApi, MainViewModel parent)
        {
            _navigator = navigator;

            if (_navigator != null)
                _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);

            OnLogoutCommand = new OnLogoutCommand(restApi);
            OnOpenLogoutDialogCommand = new OnOpenLogoutDialogCommand();
            OnBackToMainMenuCommand = new OnOpenMainMenuCommand(parent);

            OnOpenListDialogCommand = new OnOpenListDialogCommand(this);
            OnAddToBgProcessCommand = new OnAddToBgProcessCommand(this, restApi);
            OnRefreshListDialogCommand = new OnRefreshListDialogCommand(this);
            OnClearListDialogCommand = new OnClearListDialogCommand(this);
        }

        public void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _pdamName;
        public string PdamName
        {
            get => _pdamName;
            set
            {
                _pdamName = value;
                OnPropertyChanged();
            }
        }

        public void UpdateState()
        {
            Name = Application.Current.Resources["FullName"]?.ToString();
            PdamName = Application.Current.Resources["NamaPdam"]?.ToString();
            SetAppHeader();
        }

        private object _appHeader;
        public object AppHeader
        {
            get => _appHeader;
            set
            {
                _appHeader = value;
                OnPropertyChanged();
            }
        }

        private void SetAppHeader()
        {
            AppHeader = new
            {
                Laporan = true,
                Bantuan = true
            };
        }

        private List<BackgroundProcessHelper.ProcessObject> _bgProcessList;
        public List<BackgroundProcessHelper.ProcessObject> BgProcessList
        {
            get => _bgProcessList;
            set
            {
                _bgProcessList = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyProcess;
        public bool IsEmptyProcess
        {
            get => _isEmptyProcess;
            set
            {
                _isEmptyProcess = value;
                OnPropertyChanged();
            }
        }

        private bool _isProcessRunning;
        public bool IsProcessRunning
        {
            get => _isProcessRunning;
            set
            {
                _isProcessRunning = value;
                OnPropertyChanged();
            }
        }
    }
}
