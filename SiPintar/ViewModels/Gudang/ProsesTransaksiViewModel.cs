using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.ViewModels.Gudang
{
    public class ProsesTransaksiViewModel : ViewModelBase
    {
        private readonly PeriodePostingViewModel _periodePostingVm;
        private readonly PengajuanPembelianViewModel _pengajuanPembelianVm;
        private readonly SupervisiPengajuanPembelianViewModel _pengajuanBeliBarangVm;
        private readonly TransaksiBarangKeluarViewModel _transaksiBarangKeluarVm;
        private readonly DaftarBarangMasukViewModel _daftarBarangMasukVm;
        private readonly DaftarBarangKeluarViewModel _daftarBarangKeluarVm;
        private readonly DistribusiBarangViewModel _distribusiBarangVm;

        public ProsesTransaksiViewModel(IRestApiClientModel? restApi = null, bool? isTest = false)
        {
            _periodePostingVm = new PeriodePostingViewModel(this, restApi!, isTest ?? false);
            _pengajuanPembelianVm = new PengajuanPembelianViewModel(this, restApi!, isTest ?? false);
            _pengajuanBeliBarangVm = new SupervisiPengajuanPembelianViewModel(this, restApi!, isTest ?? false);
            _transaksiBarangKeluarVm = new TransaksiBarangKeluarViewModel(this, restApi!, isTest ?? false);
            _daftarBarangMasukVm = new DaftarBarangMasukViewModel(this, restApi!, isTest ?? false);
            _daftarBarangKeluarVm = new DaftarBarangKeluarViewModel(this, restApi!, isTest ?? false);
            _distribusiBarangVm = new DistribusiBarangViewModel(this, restApi!, isTest ?? false);
        }

        private ViewModelBase? _pageViewModel;
        public ViewModelBase? PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private List<HorizontalNavigationItem>? _navigationItems;
        public List<HorizontalNavigationItem>? NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Periode" => _periodePostingVm,
                "Pembelian Pengajuan" => _pengajuanPembelianVm,
                "Pembelian Supervisi" => _pengajuanBeliBarangVm,
                "Transaksi Barang Keluar" => _transaksiBarangKeluarVm,
                "Daftar Barang Masuk" => _daftarBarangMasukVm,
                "Daftar Barang Keluar" => _daftarBarangKeluarVm,
                "Distribusi Barang" => _distribusiBarangVm,
                _ => null
            };
            LoadPageCommand(pageName);
        }
        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Periode", IsSelected = false },
                new HorizontalNavigationItem() {Label = "Pembelian", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                {
                    new HorizontalNavigationItem() {Label = "Pengajuan"},
                    new HorizontalNavigationItem() {Label = "Supervisi"},
                }},
                new HorizontalNavigationItem() { Label = "Transaksi Barang Keluar", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Daftar Barang Masuk", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Daftar Barang Keluar", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Distribusi Barang", IsSelected = false },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Periode":
                        {
                            if (PageViewModel is PeriodePostingViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Pembelian Pengajuan":
                        {
                            if (PageViewModel is PengajuanPembelianViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Pembelian Supervisi":
                        {
                            if (PageViewModel is SupervisiPengajuanPembelianViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Transaksi Barang Keluar":
                        ((TransaksiBarangKeluarViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Daftar Barang Masuk":
                        ((DaftarBarangMasukViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Daftar Barang Keluar":
                        ((DaftarBarangKeluarViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Distribusi Barang":
                        ((DistribusiBarangViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
