using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class WilayahAdministrasiViewModel : ViewModelBase
    {
        private readonly AreaWilayahViewModel _areaWilayah;
        private readonly KelurahanViewModel _kelurahan;
        private readonly BlokViewModel _blok;
        private readonly DmaViewModel _dma;
        private readonly DmzViewModel _dmz;
        public WilayahAdministrasiViewModel(AtributViewModel parentViewModel, IRestApiClientModel _restApi)
        {
            _ = parentViewModel;

            _areaWilayah = new AreaWilayahViewModel(this, _restApi);
            _kelurahan = new KelurahanViewModel(this, _restApi);
            _blok = new BlokViewModel(this, _restApi);
            _dma = new DmaViewModel(this, _restApi);
            _dmz = new DmzViewModel(this, _restApi);
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Area Wilayah" => _areaWilayah,
                "Kelurahan" => _kelurahan,
                "Blok" => _blok,
                "District Meter Area" => _dma,
                "District Meter Zona" => _dmz,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Area Wilayah":
                        ((AreaWilayahViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Kelurahan":
                        ((KelurahanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Blok":
                        ((BlokViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "District Meter Area":
                        ((DmaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "District Meter Zona":
                        ((DmzViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public List<INavigationItem> GetNavigationItems()
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>();

            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Area Wilayah", Icon = PackIconKind.AccountFilter, IsSelected = true, IconForeground = "#F5A629" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Kelurahan", Icon = PackIconKind.HomeGroup, IsSelected = false, IconForeground = "#028DDB" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Blok", Icon = PackIconKind.Routes, IsSelected = false, IconForeground = "#4BCA81" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "District Meter Area", Icon = PackIconKind.MapMarker, IsSelected = false, IconForeground = "#3CB1F2" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "District Meter Zona", Icon = PackIconKind.MapMarkerRadius, IsSelected = false, IconForeground = "#A51D16" }
            );

            return Nav;
        }
    }
}
