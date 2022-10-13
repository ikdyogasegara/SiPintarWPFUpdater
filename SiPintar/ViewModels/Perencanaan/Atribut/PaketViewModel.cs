using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.ViewModels.Perencanaan.Atribut
{

    [ExcludeFromCodeCoverage]
    public class PaketViewModel : ViewModelBase
    {
        private readonly PaketMaterialViewModel _paketMaterial;
        private readonly PaketOngkosViewModel _paketOngkos;
        private readonly PaketRabViewModel _paketRab;
        public PaketViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _paketMaterial = new PaketMaterialViewModel(this, restApi);
            _paketOngkos = new PaketOngkosViewModel(this, restApi);
            _paketRab = new PaketRabViewModel(this, restApi);
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
                "Material" => _paketMaterial,
                "Ongkos" => _paketOngkos,
                "RAB" => _paketRab,

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
                    case "Material":
                        ((PaketMaterialViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Ongkos":
                        ((PaketOngkosViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "RAB":
                        ((PaketRabViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Material", Icon = PackIconKind.HammerScrewdriver, IsSelected = true, IconForeground = "#028DDB" },
                new ColoredFirstLevelNavigationItem() { Label = "Ongkos", Icon = PackIconKind.Tag, IsSelected = false, IconForeground = "#F5A629" },
                new ColoredFirstLevelNavigationItem() { Label = "RAB", Icon = PackIconKind.BookOpenVariant, IsSelected = false, IconForeground = "#338D5D" }
            };

            return Nav;
        }
    }
}
