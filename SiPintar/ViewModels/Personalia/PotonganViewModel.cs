using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Potongan;

namespace SiPintar.ViewModels.Personalia
{
    public class PotonganViewModel : ViewModelBase
    {
        public readonly MasterPotonganViewModel _masterPotongan;
        public readonly PotonganBulananViewModel _PotonganBulanan;
        public readonly PengecualianPotonganViewModel _pengecualianPotongan;

        public PotonganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _masterPotongan = new MasterPotonganViewModel(restApi, isTest);
            _PotonganBulanan = new PotonganBulananViewModel(restApi, isTest);
            _pengecualianPotongan = new PengecualianPotonganViewModel(restApi, isTest);
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
                "Master Potongan" => _masterPotongan,
                "Potongan Bulanan" => _PotonganBulanan,
                "Pengecualian Potongan" => _pengecualianPotongan,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Master Potongan", IsSelected = true},
                new HorizontalNavigationItem() { Label = "Potongan Bulanan" },
                new HorizontalNavigationItem() { Label = "Pengecualian Potongan" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Master Potongan":
                        ((MasterPotonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Potongan Bulanan":
                        ((PotonganBulananViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pengecualian Potongan":
                        ((PengecualianPotonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
