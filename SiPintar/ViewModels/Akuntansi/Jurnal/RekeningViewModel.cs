using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.ViewModels.Akuntansi.Jurnal
{
    public class RekeningViewModel : VmBase
    {
        private readonly RekAirViewModel _rekAir;
        private readonly RekNonAirViewModel _rekNonAir;

        public RekeningViewModel(JurnalViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            _rekAir = new RekAirViewModel(this, restApi);
            _rekNonAir = new RekNonAirViewModel(this, restApi);
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
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
                "Rek. Air (JRA)" => _rekAir,
                "Rek. Non-Air (JRNA)" => _rekNonAir,
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
                    case "Rek. Air (JRA)":
                        ((RekAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Rek. Non-Air (JRNA)":
                        ((RekNonAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        public List<INavigationItem> GetNavigationItems()
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>
            {
                new ColoredFirstLevelNavigationItem() { Label = "Rek. Air (JRA)", Icon = PackIconKind.Water, IsSelected = false, IconForeground = "#3CB1F2"},
                new ColoredFirstLevelNavigationItem() { Label = "Rek. Non-Air (JRNA)", Icon = PackIconKind.WaterRemove, IsSelected = false, IconForeground = "#F5A629"}
            };

            return Nav;
        }
    }
}
