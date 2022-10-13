using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;

namespace SiPintar.ViewModels.Akuntansi.Jurnal
{
    public class InstalasiViewModel : VmBase
    {
        private readonly BahanKimiaViewModel _bahanKimia;
        private readonly DaftarHutangViewModel _daftarHutang;

        public InstalasiViewModel(JurnalViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            _bahanKimia = new BahanKimiaViewModel(this, restApi);
            _daftarHutang = new DaftarHutangViewModel(this, restApi);
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
                "Bahan Kimia &\nInstalasi (JPBIK)" => _bahanKimia,
                "Daftar Hutang &\nHarus Dibayar (DHHD)" => _daftarHutang,
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
                    case "Bahan Kimia &\nInstalasi (JPBIK)":
                        ((BahanKimiaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Daftar Hutang &\nHarus Dibayar (DHHD)":
                        ((DaftarHutangViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Bahan Kimia &\nInstalasi (JPBIK)", Icon = PackIconKind.Warehouse, IsSelected = false, IconForeground = "#A66335"},
                new ColoredFirstLevelNavigationItem() { Label = "Daftar Hutang &\nHarus Dibayar (DHHD)", Icon = PackIconKind.CashMultiple, IsSelected = false, IconForeground = "#028DDB"}
            };

            return Nav;
        }
    }
}
