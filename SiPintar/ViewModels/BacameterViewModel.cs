using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Authentication;
using SiPintar.Commands.Bacameter.Navigator;
using SiPintar.Commands.Global.BackgroundProcess;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.ViewModels.Bacameter.Factories;

namespace SiPintar.ViewModels
{
    public class BacameterViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        public OnboardingViewModel _onboardingViewModel;

        public ViewModelBase CurrentViewModel => _navigator?.BacameterCurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand OnLogoutCommand { get; }
        public ICommand OnOpenLogoutDialogCommand { get; }
        public ICommand OnBackToMainMenuCommand { get; }
        public ICommand OnOpenListDialogCommand { get; }
        public ICommand OnAddToBgProcessCommand { get; }
        public ICommand OnRefreshListDialogCommand { get; }
        public ICommand OnClearListDialogCommand { get; }

        public BacameterViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IRestApiClientModel restApi, MainViewModel parent)
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

            _onboardingViewModel = new OnboardingViewModel(this);
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
            SetAppHeader();
        }

        [ExcludeFromCodeCoverage]
        private async Task CheckOnBoardingAsync()
        {
            var isOnboardBefore = Application.Current.Resources["OnBoardBacameterFlag"].ToString();
            if (isOnboardBefore == "0")
            {
                await Task.Delay(10);
                _onboardingViewModel.OpenPageCommand.Execute("welcome");
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
            bool HasSupervisiPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Bacameter.Supervisi",
            });

            bool HasProduktivitasPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Bacameter.Produktivitas",
            });

            bool HasPemetaanPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Bacameter.Pemetaan_Pelanggan",
            });

            bool HasSistemKontrolPermission = PermissionHelper.HasPermission(new List<string>()
            {
                "Bacameter.Rute_Baca_Meter",
                "Bacameter.Atribut_Tarif_Golongan",
                "Bacameter.Atribut_Petugas_Baca",
                "Bacameter.Atribut_Daftar_Kelainan",
                "Bacameter.Atribut_Data_Pelanggan",
                "Bacameter.Laporan_Distribusi_Pelanggan",
                "Bacameter.Laporan_Jadwal_Rute_Baca",
                "Bacameter.Pengaturan_Umum",
            });

            AppHeader = new
            {
                Supervisi = HasSupervisiPermission,
                Produktivitas = HasProduktivitasPermission,
                PemetaanPelanggan = HasPemetaanPermission,
                SistemKontrol = HasSistemKontrolPermission,
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
