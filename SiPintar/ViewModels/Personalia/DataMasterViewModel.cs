using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.ViewModels.Personalia
{
    public class DataMasterViewModel : ViewModelBase
    {
        public readonly PegawaiViewModel _dataPegawai;
        public readonly KeluargaViewModel _dataKeluarga;
        public readonly JabatanViewModel _masterJabatan;
        public readonly DepartemenViewModel _masterDepartemen;
        public readonly DivisiViewModel _masterDivisi;
        public readonly AreaKerjaViewModel _masterAreaKerja;
        public readonly GolonganViewModel _masterGolongan;
        public readonly PendidikanViewModel _masterPendidikan;
        public readonly TipeKeluargaViewModel _tipeKeluarga;

        public DataMasterViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _dataPegawai = new PegawaiViewModel(restApi, isTest);
            _dataKeluarga = new KeluargaViewModel(restApi, isTest);
            _masterJabatan = new JabatanViewModel(restApi, isTest);
            _masterDepartemen = new DepartemenViewModel(restApi, isTest);
            _masterDivisi = new DivisiViewModel(restApi, isTest);
            _masterAreaKerja = new AreaKerjaViewModel(restApi, isTest);
            _masterGolongan = new GolonganViewModel(restApi, isTest);
            _masterPendidikan = new PendidikanViewModel(restApi, isTest);
            _tipeKeluarga = new TipeKeluargaViewModel(restApi, isTest);
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
                "Pegawai" => _dataPegawai,
                "Keluarga" => _dataKeluarga,
                "Jabatan" => _masterJabatan,
                "Departemen" => _masterDepartemen,
                "Divisi" => _masterDivisi,
                "Area Kerja" => _masterAreaKerja,
                "Golongan" => _masterGolongan,
                "Pendidikan" => _masterPendidikan,
                "Tipe Keluarga" => _tipeKeluarga,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Pegawai", IsSelected = true },
                new HorizontalNavigationItem() { Label = "Keluarga" },
                new HorizontalNavigationItem() { Label = "Jabatan" },
                new HorizontalNavigationItem() { Label = "Departemen" },
                new HorizontalNavigationItem() { Label = "Divisi" },
                new HorizontalNavigationItem() { Label = "Area Kerja" },
                new HorizontalNavigationItem() { Label = "Golongan" },
                new HorizontalNavigationItem() { Label = "Pendidikan" },
                new HorizontalNavigationItem() { Label = "Tipe Keluarga" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Pegawai":
                        ((PegawaiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Keluarga":
                        ((KeluargaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Jabatan":
                        ((JabatanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Departemen":
                        ((DepartemenViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Divisi":
                        ((DivisiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Area Kerja":
                        ((AreaKerjaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Golongan":
                        ((GolonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pendidikan":
                        ((PendidikanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tipe Keluarga":
                        ((TipeKeluargaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
