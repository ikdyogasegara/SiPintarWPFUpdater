using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.ViewModels.Gudang
{
    public class MasterDataViewModel : ViewModelBase
    {
        private readonly BarangViewModel BarangVm;
        private readonly BarangHabisViewModel BarangHabisVm;
        private readonly TipeBarangViewModel TipeBarangVm;
        private readonly JenisBarangViewModel JenisBarangVm;
        private readonly DiameterViewModel DiameterVm;
        private readonly SupplierViewModel SupplierVm;
        private readonly PaketViewModel PaketVm;
        private readonly KategoriBarangMasukViewModel KategoriBarangMasukVm;
        private readonly KategoriBarangKeluarViewModel KategoriBarangKeluarVm;
        private readonly BagianMemintaBarangViewModel BagianMemintaBarangVm;

        public MasterDataViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            BarangVm = new BarangViewModel(this, restApi, isTest);
            BarangHabisVm = new BarangHabisViewModel(this, restApi, isTest);
            TipeBarangVm = new TipeBarangViewModel(this, restApi, isTest);
            JenisBarangVm = new JenisBarangViewModel(this, restApi, isTest);
            DiameterVm = new DiameterViewModel(this, restApi, isTest);
            PaketVm = new PaketViewModel(this, restApi, isTest);
            SupplierVm = new SupplierViewModel(this, restApi, isTest);
            KategoriBarangMasukVm = new KategoriBarangMasukViewModel(this, restApi, isTest);
            KategoriBarangKeluarVm = new KategoriBarangKeluarViewModel(this, restApi, isTest);
            BagianMemintaBarangVm = new BagianMemintaBarangViewModel(this, restApi, isTest);
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private List<HorizontalNavigationItem> _navigationItems;
        public List<HorizontalNavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Barang" => BarangVm,
                "Barang Habis" => BarangHabisVm,
                "Tipe Barang" => TipeBarangVm,
                "Jenis Barang" => JenisBarangVm,
                "Diameter" => DiameterVm,
                "Supplier" => SupplierVm,
                "Paket" => PaketVm,
                "Kategori Barang Masuk" => KategoriBarangMasukVm,
                "Kategori Barang Keluar" => KategoriBarangKeluarVm,
                "Bagian Meminta Barang" => BagianMemintaBarangVm,
                _ => null
            };
            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Barang", IsSelected = true },
                new HorizontalNavigationItem() { Label = "Barang Habis", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Tipe Barang", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Jenis Barang", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Diameter", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Supplier", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Paket", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Kategori Barang Masuk", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Kategori Barang Keluar", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Bagian Meminta Barang", IsSelected = false },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Barang":
                        ((BarangViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Barang Habis":
                        ((BarangHabisViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tipe Barang":
                        ((TipeBarangViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Jenis Barang":
                        ((JenisBarangViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Diameter":
                        ((DiameterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Supplier":
                        ((SupplierViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Paket":
                        ((PaketViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Kategori Barang Keluar":
                        ((KategoriBarangKeluarViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Bagian Meminta Barang":
                        ((BagianMemintaBarangViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
