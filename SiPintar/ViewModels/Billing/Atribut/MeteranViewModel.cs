using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Meteran;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class MeteranViewModel : ViewModelBase
    {
        private readonly MerkMeterViewModel _merk;
        private readonly KondisiMeterViewModel _kondisi;
        public MeteranViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _merk = new MerkMeterViewModel(this, restApi);
            _kondisi = new KondisiMeterViewModel(this, restApi);
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
                "Merk Meter" => _merk,
                "Kondisi Meter" => _kondisi,
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
                    case "Merk Meter":
                        ((MerkMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Kondisi Meter":
                        ((KondisiMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Merk Meter", Icon = PackIconKind.PencilBox, IsSelected = true, IconForeground = "#028DDB" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Kondisi Meter", Icon = PackIconKind.HammerWrench, IsSelected = false, IconForeground = "#F5A629" }
            );

            return Nav;
        }
    }
}
