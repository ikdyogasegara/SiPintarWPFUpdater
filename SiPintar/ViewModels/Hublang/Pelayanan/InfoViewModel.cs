using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Hublang.Pelayanan.Info;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class InfoViewModel : ViewModelBase
    {
        private readonly HistoriPembacaanViewModel _historiPembacaan;
        private readonly HistoriPembayaranViewModel _historiPembayaran;
        private readonly HistoriPermohonanViewModel _historiPermohonan;
        private readonly DataLanggananViewModel _dataLangganan;
        private readonly TagihanViewModel _tagihan;
        private readonly DenahViewModel _denah;

        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public InfoViewModel(PelayananViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            _restApi = restApi;
            _isTest = isTest;
            _historiPembacaan = new HistoriPembacaanViewModel(this, restApi, isTest);
            _historiPembayaran = new HistoriPembayaranViewModel(this, restApi, isTest);
            _historiPermohonan = new HistoriPermohonanViewModel(this, restApi, isTest);
            _dataLangganan = new DataLanggananViewModel(this, restApi, isTest);
            _tagihan = new TagihanViewModel(this, restApi, isTest);
            _denah = new DenahViewModel(this, restApi, isTest);
            OnOpenPilihPelangganCommand = new OnOpenPilihPelangganCommand(this, restApi, isTest);
        }

        public ICommand OnOpenPilihPelangganCommand { get; }

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get => _selectedPelanggan;
            set
            {
                _selectedPelanggan = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public async Task UpdatePageAsync(string pageName)
        {
            if (!string.IsNullOrWhiteSpace(pageName))
            {
                if (SelectedPelanggan == null)
                {
                    var res = await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstanceSelectPelanggan);
                    if (SelectedPelanggan != null) { _ = UpdatePageAsync(pageName); }
                }
                else
                {
                    PageViewModel = pageName switch
                    {
                        "Data Langganan" => _dataLangganan,
                        "Pembacaan" => _historiPembacaan,
                        "Pembayaran Air" => _historiPembayaran,
                        "Pembayaran Limbah" => _historiPembayaran,
                        "Pembayaran L2T2" => _historiPembayaran,
                        "Pembayaran Non Air" => _historiPembayaran,
                        "Permohonan,SPK,BA" => _historiPermohonan,
                        "Tagihan" => _tagihan,
                        "Denah RAB" => _denah,
                        _ => null
                    };
                    LoadPageCommand(pageName);
                }
            }
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Pembacaan":
                        ((HistoriPembacaanViewModel)PageViewModel).SetupData();
                        ((HistoriPembacaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran Air":
                        ((HistoriPembayaranViewModel)PageViewModel).SetupData("Air");
                        ((HistoriPembayaranViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran Limbah":
                        ((HistoriPembayaranViewModel)PageViewModel).SetupData("Limbah");
                        ((HistoriPembayaranViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran L2T2":
                        ((HistoriPembayaranViewModel)PageViewModel).SetupData("Lltt");
                        ((HistoriPembayaranViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran Non Air":
                        ((HistoriPembayaranViewModel)PageViewModel).SetupData("Non Air");
                        ((HistoriPembayaranViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Permohonan,SPK,BA":
                        ((HistoriPermohonanViewModel)PageViewModel).SetupData();
                        ((HistoriPermohonanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Data Langganan":
                        ((DataLanggananViewModel)PageViewModel).SetupData();
                        ((DataLanggananViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tagihan":
                        ((TagihanViewModel)PageViewModel).SetupData();
                        ((TagihanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Denah RAB":
                        ((DenahViewModel)PageViewModel).SetupData();
                        ((DenahViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public List<INavigationItem> GetNavigationItems(string select = null)
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>
            {
                new FirstLevelNavigationItem() {
                    Label = "Data Langganan",
                    Icon = PackIconKind.ChartBox,
                    IsSelected = !string.IsNullOrWhiteSpace(select) && select == "Data Langganan"},

                new FirstLevelNavigationItem() { Label = "Pembacaan", Icon = PackIconKind.HumanMaleBoard, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pembayaran Air", Icon = PackIconKind.ChartBox, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pembayaran Limbah", Icon = PackIconKind.ChartBox, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pembayaran L2T2", Icon = PackIconKind.ChartBox, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pembayaran Non Air", Icon = PackIconKind.ChartBox, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Tagihan", Icon = PackIconKind.CreditCardOutline, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Permohonan,SPK,BA", Icon = PackIconKind.BoxCheckOutline, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Denah RAB", Icon = PackIconKind.ImageAlbum, IsSelected = false },
            };

            return Nav;
        }

        [ExcludeFromCodeCoverage]
        private object GetInstanceSelectPelanggan() => new PilihPelangganDialog(_restApi, SetSelectedPelanggan);

        public bool SetSelectedPelanggan(dynamic param)
        {
            SelectedPelanggan = param;
            return true;
        }
    }
}
