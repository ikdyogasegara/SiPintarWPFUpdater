using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.ViewModels.Personalia
{
    public class KepegawaianViewModel : ViewModelBase
    {
        public readonly SKCalonPegawaiViewModel _skCalonPegawai;
        public readonly SKPegawaiTetapViewModel _skPegawaiTetap;
        public readonly PangkatBerkalaViewModel _pangkatBerkala;
        public readonly PangkatPilihanViewModel _pangkatPilihan;
        public readonly PensiunViewModel _pensiun;
        public readonly MutasiJabatanViewModel _mutasiJabatan;
        public readonly PerjalananDinasViewModel _perjalananDinas;
        public readonly DiklatPelatihanViewModel _diklatPelatihan;
        public readonly PostingPersonaliaViewModel _postingPersonalia;
        public readonly Kepegawaian.LaporanViewModel _laporan;

        public KepegawaianViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _skCalonPegawai = new SKCalonPegawaiViewModel(restApi, isTest);
            _skPegawaiTetap = new SKPegawaiTetapViewModel(restApi, isTest);
            _pangkatBerkala = new PangkatBerkalaViewModel(restApi, isTest);
            _pangkatPilihan = new PangkatPilihanViewModel(restApi, isTest);
            _pensiun = new PensiunViewModel(restApi, isTest);
            _mutasiJabatan = new MutasiJabatanViewModel(restApi, isTest);
            _perjalananDinas = new PerjalananDinasViewModel(restApi, isTest);
            _diklatPelatihan = new DiklatPelatihanViewModel(restApi, isTest);
            _postingPersonalia = new PostingPersonaliaViewModel(restApi, isTest);
            _laporan = new Kepegawaian.LaporanViewModel(restApi, isTest);
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
                "SK Calon Pegawai" => _skCalonPegawai,
                "SK Pegawai Tetap" => _skPegawaiTetap,
                "Pangkat & Berkala" => _pangkatBerkala,
                "Pangkat Pilihan" => _pangkatPilihan,
                "Pensiun" => _pensiun,
                "Mutasi Jabatan" => _mutasiJabatan,
                "Perjalanan Dinas" => _perjalananDinas,
                "Diklat & Pelatihan" => _diklatPelatihan,
                "Posting Personalia" => _postingPersonalia,
                "Laporan" => _laporan,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "SK Calon Pegawai", IsSelected = true },
                new HorizontalNavigationItem() { Label = "SK Pegawai Tetap" },
                new HorizontalNavigationItem() { Label = "Pangkat & Berkala" },
                new HorizontalNavigationItem() { Label = "Pangkat Pilihan" },
                new HorizontalNavigationItem() { Label = "Pensiun" },
                new HorizontalNavigationItem() { Label = "Mutasi Jabatan" },
                new HorizontalNavigationItem() { Label = "Perjalanan Dinas" },
                new HorizontalNavigationItem() { Label = "Diklat & Pelatihan" },
                new HorizontalNavigationItem() { Label = "Posting Personalia" },
                new HorizontalNavigationItem() { Label = "Laporan" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "SK Calon Pegawai":
                        ((SKCalonPegawaiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "SK Pegawai Tetap":
                        ((SKPegawaiTetapViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pangkat & Berkala":
                        ((PangkatBerkalaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pangkat Pilihan":
                        ((PangkatPilihanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pensiun":
                        ((PensiunViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Mutasi Jabatan":
                        ((MutasiJabatanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Perjalanan Dinas":
                        ((PerjalananDinasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Diklat & Pelatihan":
                        ((DiklatPelatihanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Posting Personalia":
                        ((PostingPersonaliaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Laporan":
                        ((Kepegawaian.LaporanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
