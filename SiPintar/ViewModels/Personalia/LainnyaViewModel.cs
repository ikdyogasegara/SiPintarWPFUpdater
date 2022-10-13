using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Lainnya;

namespace SiPintar.ViewModels.Personalia
{
    public class LainnyaViewModel : ViewModelBase
    {
        public readonly StatusKeluargaPegawaiViewModel _statusKeluargaPegawai;
        public readonly DasarPensiunViewModel _dasarPensiun;
        public readonly DasarAskesViewModel _dasarAskes;
        public readonly DasarAstekViewModel _dasarAstek;
        public readonly DasarLainnyaViewModel _dasarLainnya;

        public LainnyaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _statusKeluargaPegawai = new StatusKeluargaPegawaiViewModel(restApi, isTest);
            _dasarPensiun = new DasarPensiunViewModel(restApi, isTest);
            _dasarAskes = new DasarAskesViewModel(restApi, isTest);
            _dasarAstek = new DasarAstekViewModel(restApi, isTest);
            _dasarLainnya = new DasarLainnyaViewModel(restApi, isTest);
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
                "Status Keluarga Pegawai" => _statusKeluargaPegawai,
                "Dasar Pensiun" => _dasarPensiun,
                "Dasar Askes" => _dasarAskes,
                "Dasar Astek" => _dasarAstek,
                "Dasar Lainnya" => _dasarLainnya,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Status Keluarga Pegawai", IsSelected = true},
                new HorizontalNavigationItem() { Label = "Dasar Pensiun" },
                new HorizontalNavigationItem() { Label = "Dasar Askes" },
                new HorizontalNavigationItem() { Label = "Dasar Astek" },
                new HorizontalNavigationItem() { Label = "Dasar Lainnya" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Status Keluarga Pegawai":
                        ((StatusKeluargaPegawaiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Dasar Pensiun":
                        ((DasarPensiunViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Dasar Askes":
                        ((DasarAskesViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Dasar Astek":
                        ((DasarAstekViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Dasar Lainnya":
                        ((DasarLainnyaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
