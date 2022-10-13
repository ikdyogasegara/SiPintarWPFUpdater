using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Authentication;
using SiPintar.Commands.Global.BackgroundProcess;
using SiPintar.Commands.Hublang.Navigator;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang;
using SiPintar.ViewModels.Hublang.Factories;

namespace SiPintar.ViewModels
{
    public class HublangViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        public readonly OnboardingViewModel OnboardingViewModel;

        public ViewModelBase CurrentViewModel => _navigator?.HublangCurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand OnLogoutCommand { get; }
        public ICommand OnOpenLogoutDialogCommand { get; }
        public ICommand OnBackToMainMenuCommand { get; }
        public ICommand OnOpenListDialogCommand { get; }
        public ICommand OnAddToBgProcessCommand { get; }
        public ICommand OnRefreshListDialogCommand { get; }
        public ICommand OnClearListDialogCommand { get; }

        public HublangViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IRestApiClientModel restApi, MainViewModel parent)
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

            OnboardingViewModel = new OnboardingViewModel(this);
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

            _ = CheckOnBoardingAsync();
        }

        [ExcludeFromCodeCoverage]
        private async Task CheckOnBoardingAsync()
        {
            var isOnboardBefore = Application.Current.Resources["OnBoardHublangFlag"].ToString();
            if (isOnboardBefore == "0")
            {
                await Task.Delay(10);
                OnboardingViewModel.OpenPageCommand.Execute("welcome");
            }
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
