using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.ViewModels.Personalia
{
    public class TunjanganViewModel : ViewModelBase
    {
        public readonly MasterTunjanganViewModel _masterTunjangan;
        public readonly TunjanganBulananViewModel _tunjanganBulanan;
        public readonly PengecualianTunjanganViewModel _pengecualianTunjangan;

        public TunjanganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _masterTunjangan = new MasterTunjanganViewModel(restApi, isTest);
            _tunjanganBulanan = new TunjanganBulananViewModel(restApi, isTest);
            _pengecualianTunjangan = new PengecualianTunjanganViewModel(restApi, isTest);
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
                "Master Tunjangan" => _masterTunjangan,
                "Tunjangan Bulanan" => _tunjanganBulanan,
                "Pengecualian Tunjangan" => _pengecualianTunjangan,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Master Tunjangan", IsSelected = true},
                new HorizontalNavigationItem() { Label = "Tunjangan Bulanan" },
                new HorizontalNavigationItem() { Label = "Pengecualian Tunjangan" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Master Tunjangan":
                        ((MasterTunjanganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tunjangan Bulanan":
                        ((TunjanganBulananViewModel)PageViewModel).OnLoadCommand.Execute("FirstLoad");
                        break;
                    case "Pengecualian Tunjangan":
                        ((PengecualianTunjanganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
