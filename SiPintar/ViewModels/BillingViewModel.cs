using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Authentication;
using SiPintar.Commands.Billing.Navigator;
using SiPintar.Commands.Global.BackgroundProcess;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing;
using SiPintar.ViewModels.Billing.Factories;

namespace SiPintar.ViewModels
{
    public class BillingViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        public OnboardingViewModel _onboardingViewModel;

        public ViewModelBase CurrentViewModel => _navigator?.BillingCurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand OnLogoutCommand { get; }
        public ICommand OnOpenLogoutDialogCommand { get; }
        public ICommand OnBackToMainMenuCommand { get; }
        public ICommand OnOpenListDialogCommand { get; }
        public ICommand OnAddToBgProcessCommand { get; }
        public ICommand OnRefreshListDialogCommand { get; }
        public ICommand OnClearListDialogCommand { get; }

        public BillingViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IRestApiClientModel restApi, MainViewModel parent, string module = "billing")
        {
            Module = module;
            _navigator = navigator;

            if (_navigator != null)
                _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator!, viewModelFactory!);

            OnLogoutCommand = new OnLogoutCommand(restApi);
            OnOpenLogoutDialogCommand = new OnOpenLogoutDialogCommand();
            OnBackToMainMenuCommand = new OnOpenMainMenuCommand(parent);

            OnOpenListDialogCommand = new OnOpenListDialogCommand(this);
            OnAddToBgProcessCommand = new OnAddToBgProcessCommand(this, restApi);
            OnRefreshListDialogCommand = new OnRefreshListDialogCommand(this);
            OnClearListDialogCommand = new OnClearListDialogCommand(this);

            _onboardingViewModel = new OnboardingViewModel(this);
        }

        public void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private string _module;
        public string Module
        {
            get => _module;
            set
            {
                _module = value;
                OnPropertyChanged();

                if (value == "billing")
                {
                    IsBilling = true;
                    IsBacameter = false;
                }
                else
                {
                    IsBilling = false;
                    IsBacameter = true;
                }
            }
        }

        private bool _isBilling;
        public bool IsBilling
        {
            get => _isBilling;
            set
            {
                _isBilling = value;
                OnPropertyChanged();
            }
        }

        private bool _isBacameter;
        public bool IsBacameter
        {
            get => _isBacameter;
            set
            {
                _isBacameter = value;
                OnPropertyChanged();
            }
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
            SetAppHeader();
        }

        [ExcludeFromCodeCoverage]
        private async Task CheckOnBoardingAsync()
        {
            var isOnboardBefore = Application.Current.Resources["OnBoardBillingFlag"].ToString();
            if (isOnboardBefore == "0")
            {
                await Task.Delay(10);
                _onboardingViewModel.OnOpenDialogCommand.Execute("welcome");
            }
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
            var hasSupervisiPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Billing.Periode",
                "Billing.Rekening_Air",
                "Billing.Rekening_Limbah",
                "Billing.Rekening_L2T2",
                "Billing.Pelanggan_Air",
                "Billing.Pelanggan_Limbah",
                "Billing.Pelanggan_L2T2",
                "Billing.Koreksi_Rekening_Air",
                "Billing.Penghapusan_Rekening",
                "Billing.Upload_Download",
                "Billing.Posting",
            });

            var hasAtributPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Billing.Atribut_Tarif",
                "Billing.Atribut_Wilayah_Administrasi",
                "Billing.Atribut_Loket",
                "Billing.Atribut_Meteran",
                "Billing.Atribut_Sumber_Air",
                "Billing.Atribut_Kolektif",
                "Billing.Atribut_Kepemilikan",
                "Billing.Atribut_Status",
                "Billing.Atribut_Flag",
            });

            AppHeader = new
            {
                Supervisi = hasSupervisiPermission,
                Atribut = hasAtributPermission,
                Produktivitas = false,
                Bantuan = true,
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
