using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.ViewModels.Personalia
{
    public class GajiPokokViewModel : ViewModelBase
    {
        public readonly PegawaiTetapViewModel _pegawaiTetap;
        public readonly CalonPegawaiViewModel _calonPegawai;
        public readonly TenagaKontrakViewModel _tenagaKontrak;
        public readonly TenagaHarianViewModel _tenagaHarian;
        public readonly DireksiViewModel _direksi;
        public readonly DewanPengawasViewModel _dewanPengawas;
        public readonly TambahanViewModel _tambahan;

        public GajiPokokViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _pegawaiTetap = new PegawaiTetapViewModel(restApi, isTest);
            _calonPegawai = new CalonPegawaiViewModel(restApi, isTest);
            _tenagaKontrak = new TenagaKontrakViewModel(restApi, isTest);
            _tenagaHarian = new TenagaHarianViewModel(restApi, isTest);
            _direksi = new DireksiViewModel(restApi, isTest);
            _dewanPengawas = new DewanPengawasViewModel(restApi, isTest);
            _tambahan = new TambahanViewModel(restApi, isTest);
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
                "Pegawai Tetap" => _pegawaiTetap,
                "Calon Pegawai" => _calonPegawai,
                "Tenaga Kontrak" => _tenagaKontrak,
                "Tenaga Harian" => _tenagaHarian,
                "Direksi" => _direksi,
                "Dewan Pengawas" => _dewanPengawas,
                "Tambahan" => _tambahan,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Pegawai Tetap", IsSelected = true},
                new HorizontalNavigationItem() { Label = "Calon Pegawai" },
                new HorizontalNavigationItem() { Label = "Tenaga Kontrak" },
                new HorizontalNavigationItem() { Label = "Tenaga Harian" },
                new HorizontalNavigationItem() { Label = "Direksi" },
                new HorizontalNavigationItem() { Label = "Dewan Pengawas" },
                new HorizontalNavigationItem() { Label = "Tambahan" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Pegawai Tetap":
                        ((PegawaiTetapViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Calon Pegawai":
                        ((CalonPegawaiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tenaga Kontrak":
                        ((TenagaKontrakViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tenaga Harian":
                        ((TenagaHarianViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Direksi":
                        ((DireksiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Dewan Pengawas":
                        ((DewanPengawasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tambahan":
                        ((TambahanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
