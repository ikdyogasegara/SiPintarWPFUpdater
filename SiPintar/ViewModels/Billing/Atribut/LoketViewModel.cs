using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Loket;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class LoketViewModel : ViewModelBase
    {
        private readonly LoketTerdaftarViewModel _terdaftar;
        private readonly LoketPembatalanViewModel _pembatalan;
        public LoketViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _terdaftar = new LoketTerdaftarViewModel(this, restApi);
            _pembatalan = new LoketPembatalanViewModel(this, restApi);
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
                "Loket Terdaftar" => _terdaftar,
                "Pembatalan trx." => _pembatalan,
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
                    case "Loket Terdaftar":
                        ((LoketTerdaftarViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembatalan trx.":
                        ((LoketPembatalanViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Loket Terdaftar", Icon = PackIconKind.Store, IsSelected = true, IconForeground = "#028DDB" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Pembatalan trx.", Icon = PackIconKind.CartOff, IsSelected = false, IconForeground = "#F5A629" }
            );

            return Nav;
        }
    }
}
