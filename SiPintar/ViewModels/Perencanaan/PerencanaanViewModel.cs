using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Commands;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.ViewModels.Perencanaan
{
    public class PerencanaanViewModel : ViewModelBase
    {
        private readonly PermohonanViewModel _permohonan;

        public PerencanaanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            var pelayanan = new PelayananViewModel(restApi, isTest);
            _permohonan = new PermohonanViewModel(pelayanan, restApi, isTest);
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
                "Surat Perintah Kerja (SPK) Pelanggan Air" => _permohonan,
                "Surat Perintah Kerja (SPK) Pelanggan Limbah" => _permohonan,
                "Surat Perintah Kerja (SPK) Pelanggan L2T2" => _permohonan,
                "Surat Perintah Kerja (SPK) Non Pelanggan" => _permohonan,

                "Rencana Anggaran Belanja (RAB) Pelanggan Air" => _permohonan,
                "Rencana Anggaran Belanja (RAB) Pelanggan Limbah" => _permohonan,
                "Rencana Anggaran Belanja (RAB) Pelanggan L2T2" => _permohonan,
                "Rencana Anggaran Belanja (RAB) Non Pelanggan" => _permohonan,
                _ => null
            };

            _ = LoadPageCommandAsync(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem()
                {
                    Label = "Surat Perintah Kerja (SPK)",
                    IsSelected = true,
                    ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                    {
                        new HorizontalNavigationItem() { Label = "Pelanggan Air",IsSelected = true },
                        new HorizontalNavigationItem() { Label = "Pelanggan Limbah" },
                        new HorizontalNavigationItem() { Label = "Pelanggan L2T2" },
                        new HorizontalNavigationItem() { Label = "Non Pelanggan" },
                    }
                },
                new HorizontalNavigationItem()
                {
                    Label = "Rencana Anggaran Belanja (RAB)",
                    IsSelected = false,
                    ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                    {
                        new HorizontalNavigationItem() { Label = "Pelanggan Air", IsSelected = true  },
                        new HorizontalNavigationItem() { Label = "Pelanggan Limbah" },
                        new HorizontalNavigationItem() { Label = "Pelanggan L2T2" },
                        new HorizontalNavigationItem() { Label = "Non Pelanggan"}
                    }
                },
            };

            return nav;
        }

        private Task LoadPageCommandAsync(string pageName)
        {
            switch (pageName)
            {
                case "Surat Perintah Kerja (SPK) Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "SPK" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Surat Perintah Kerja (SPK) Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "SPK" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Surat Perintah Kerja (SPK) Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "SPK" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Surat Perintah Kerja (SPK) Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "SPK" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;

                case "Rencana Anggaran Belanja (RAB) Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "RAB" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;
                case "Rencana Anggaran Belanja (RAB) Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "RAB" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Rencana Anggaran Belanja (RAB) Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "RAB" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Rencana Anggaran Belanja (RAB) Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "RAB" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;

                default:
                    _ = pageName;
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
