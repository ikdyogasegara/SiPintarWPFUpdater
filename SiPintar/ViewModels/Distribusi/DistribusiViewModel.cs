using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.ViewModels.Hublang;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.ViewModels.Distribusi
{
    public class DistribusiViewModel : ViewModelBase
    {
        public readonly GantiMeterViewModel _gantiMeter;
        public readonly GantiMeterNonRutinViewModel _gantiMeterNonRutin;
        private readonly PermohonanViewModel _permohonan;
        private readonly RotasiMeterViewModel _rotasiMeter;

        public DistribusiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _gantiMeter = new GantiMeterViewModel(restApi);
            _gantiMeterNonRutin = new GantiMeterNonRutinViewModel(restApi);

            var pelayanan = new PelayananViewModel(restApi, isTest);
            _permohonan = new PermohonanViewModel(pelayanan, restApi, isTest);
            _rotasiMeter = new RotasiMeterViewModel(pelayanan, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }

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
                "Usulan" => _permohonan,
                "Berita Acara Pelanggan Air" => _permohonan,
                "Berita Acara Non Pelanggan" => _permohonan,
                "Berita Acara Pelanggan Limbah" => _permohonan,
                "Berita Acara Pelanggan L2T2" => _permohonan,
                "Rotasi Meter" => _rotasiMeter,
                "Ganti Meter" => _gantiMeter,
                "Ganti Meter Non Rutin" => _gantiMeterNonRutin,
                _ => null
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>();

            nav.Add(new HorizontalNavigationItem() { Label = "Usulan", IsSelected = true });

            nav.Add(new HorizontalNavigationItem()
            {
                Label = "Berita Acara",
                IsSelected = false,
                ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                {
                    new HorizontalNavigationItem() { Label = "Pelanggan Air", IsSelected = true  },
                    new HorizontalNavigationItem() { Label = "Pelanggan Limbah" },
                    new HorizontalNavigationItem() { Label = "Pelanggan L2T2" },
                    new HorizontalNavigationItem() { Label = "Non Pelanggan"},
                }
            });

            nav.Add(new HorizontalNavigationItem() { Label = "Rotasi Meter" });
            nav.Add(new HorizontalNavigationItem() { Label = "Ganti Meter" });
            nav.Add(new HorizontalNavigationItem() { Label = "Ganti Meter Non Rutin" });

            return nav;
        }

        private void LoadPageCommand(string pageName)
        {
            switch (pageName)
            {
                case "Usulan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Usulan" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Berita Acara Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Berita Acara Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Berita Acara Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Berita Acara Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;

                case "Rotasi Meter":
                    _ = ((AsyncCommandBase)((RotasiMeterViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                    break;

                case "Ganti Meter":
                    ((GantiMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Ganti Meter Non Rutin":
                    ((GantiMeterNonRutinViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                default:
                    _ = pageName;
                    break;
            }
        }
    }
}
