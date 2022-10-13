using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiPintar.Commands.Authentication;
using SiPintar.Commands.Main;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Background;
using SiPintar.ViewModels.Main;

namespace SiPintar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public BacameterViewModel bacameter;
        public BillingViewModel billing;
        public HublangViewModel hublang;
        public LoketViewModel loket;
        public DistribusiViewModel distribusi;
        public PerencanaanViewModel perencanaan;
        public GudangViewModel gudang;
        public PersonaliaViewModel personalia;
        public AkuntansiViewModel akuntansi;
        public LaporanViewModel laporan;

        public readonly DesktopViewModel Desktop;
        private readonly WebViewModel _web;
        private readonly DaftarPenggunaViewModel _daftarPengguna;
        private readonly ProfilSayaViewModel _profilSaya;
        private readonly ManajementParafViewModel _manajementParaf;


        public ICommand OnLoadCommand { get; }
        public ICommand OnLogoutCommand { get; }
        public ICommand OnOpenLogoutDialogCommand { get; }

        public INavigator Navi { get; }

        public MainViewModel(
            INavigator navigator,
            Bacameter.Factories.IViewModelFactory bacameterFactory,
            Billing.Factories.IViewModelFactory billingFactory,
            Hublang.Factories.IViewModelFactory hublangFactory,
            Loket.Factories.IViewModelFactory loketFactory,
            Distribusi.Factories.IViewModelFactory distribusiFactory,
            Perencanaan.Factories.IViewModelFactory perencanaanFactory,
            Gudang.Factories.IViewModelFactory gudangFactory,
            Personalia.Factories.IViewModelFactory personaliaFactory,
            Akuntansi.Factories.IViewModelFactory akuntansiFactory,
            Laporan.Factories.IViewModelFactory laporanFactory,
            IRestApiClientModel restApi,
            IBackgroundService bgService
        )
        {
            Navi = navigator;

            bacameter = new BacameterViewModel(navigator, bacameterFactory, restApi, this);
            billing = new BillingViewModel(navigator, billingFactory, restApi, this, "billing");
            hublang = new HublangViewModel(navigator, hublangFactory, restApi, this);
            loket = new LoketViewModel(navigator, loketFactory, restApi, bgService, this);
            distribusi = new DistribusiViewModel(navigator, distribusiFactory, restApi, this);
            perencanaan = new PerencanaanViewModel(navigator, perencanaanFactory, restApi, this);
            gudang = new GudangViewModel(navigator, gudangFactory, restApi, this);
            personalia = new PersonaliaViewModel(navigator, personaliaFactory, restApi, this);
            akuntansi = new AkuntansiViewModel(navigator, akuntansiFactory, restApi, this);
            laporan = new LaporanViewModel(navigator, laporanFactory, restApi, this);

            Desktop = new DesktopViewModel(this);
            _web = new WebViewModel(this, restApi);
            _daftarPengguna = new DaftarPenggunaViewModel(this, restApi);
            _profilSaya = new ProfilSayaViewModel(this, restApi);
            _manajementParaf = new ManajementParafViewModel(restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnLogoutCommand = new OnLogoutCommand(restApi);
            OnOpenLogoutDialogCommand = new OnOpenLogoutDialogCommand();
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
        }

        private WindowState _windowState = WindowState.Normal;
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                OnPropertyChanged();
            }
        }

        private bool _isMinimizeToTray;
        public bool IsMinimizeToTray
        {
            get => _isMinimizeToTray;
            set
            {
                _isMinimizeToTray = value;
                OnPropertyChanged();
            }
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public List<INavigationItem> GetNavigationItems()
        {
            var role = Application.Current.Resources["NamaRole"]?.ToString();

            var Nav = new List<INavigationItem>()
            {
                new SubheaderNavigationItem() { Subheader = "APLIKASI" }
            };

            Nav.Add(
                new FirstLevelNavigationItem() { Label = "Desktop", Icon = PackIconKind.CalendarRange, IsSelected = true }
            );
            Nav.Add(
                new FirstLevelNavigationItem() { Label = "Web", Icon = PackIconKind.TableCog, IsSelected = false }
            );

            Nav.Add(
                new SubheaderNavigationItem() { Subheader = "PENGATURAN" }
            );

            if (role != null && role.ToLower().Contains("admin"))
            {
                Nav.Add(
                    new FirstLevelNavigationItem() { Label = "Daftar Pengguna", Icon = PackIconKind.Gear, IsSelected = false }
                );
            }

            Nav.Add(
                new FirstLevelNavigationItem() { Label = "Profil Saya", Icon = PackIconKind.User, IsSelected = false }
            );

            Nav.Add(
                new FirstLevelNavigationItem() { Label = "Manajement Paraf", Icon = PackIconKind.Signature, IsSelected = false }
            );

            return Nav;
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName, bool isTest = false)
        {
            if (pageName != "Desktop")
            {
                if (!Application.Current.Resources.Contains("CURRENT_MODULE"))
                    Application.Current.Resources.Add("CURRENT_MODULE", "DEFAULT");
                else
                    Application.Current.Resources["CURRENT_MODULE"] = "DEFAULT";
            }


            PageViewModel = pageName switch
            {
                "Desktop" => Desktop,
                "Web" => _web,
                "Daftar Pengguna" => _daftarPengguna,
                "Profil Saya" => _profilSaya,
                "Manajement Paraf" => _manajementParaf,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName, isTest);
        }

        private void LoadPageCommand(string pageName, bool isTest = false)
        {
            AppDispatcherHelper.Run(isTest, () =>
            {
                OnLoadCommand.Execute(null);
            });
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Desktop":
                        ((DesktopViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Web":
                        ((WebViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Daftar Pengguna":
                        ((DaftarPenggunaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Profil Saya":
                        ((ProfilSayaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Manajement Paraf":
                        ((ManajementParafViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }
    }

    [ExcludeFromCodeCoverage]
    public class HorizontalNavigationItem : ViewModelBase
    {
        public string Label { get; set; }
        public string Tooltip { get; set; }
        public bool IsSelected { get; set; }
        private int _totalBadge;
        public int TotalBadge
        {
            get { return _totalBadge; }
            set
            {
                _totalBadge = value;
                OnPropertyChanged();
            }
        }
        public string Color { get; set; } = "#028DDB";
        public ObservableCollection<HorizontalNavigationItem> ChildNavigation { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ColoredFirstLevelNavigationItem : FirstLevelNavigationItem
    {
        public string IconForeground { get; set; }
    }
}
