using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.ViewModels.Distribusi.Atribut
{
    public class KelainanGantiMeterViewModel : ViewModelBase
    {
        private readonly GantiMeterRutinViewModel _gantiMeterRutin;
        private readonly GantiMeterNonRutinViewModel _gantiMeterNonRutin;

        public KelainanGantiMeterViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _gantiMeterRutin = new GantiMeterRutinViewModel(this, restApi);
            _gantiMeterNonRutin = new GantiMeterNonRutinViewModel(this, restApi);
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
                "Rutin" => _gantiMeterRutin,
                "Non Rutin" => _gantiMeterNonRutin,

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
                    case "Rutin":
                        ((GantiMeterRutinViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Non Rutin":
                        ((GantiMeterNonRutinViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public List<INavigationItem> GetNavigationItems()
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>
            {
                new ColoredFirstLevelNavigationItem() { Label = "Rutin", Icon = PackIconKind.RepeatOne, IsSelected = true, IconForeground = "#F2542A" },
                new ColoredFirstLevelNavigationItem() { Label = "Non Rutin", Icon = PackIconKind.SpeedometerSlow, IsSelected = false, IconForeground = "#1E7B49" },
            };

            return Nav;
        }
    }

}
