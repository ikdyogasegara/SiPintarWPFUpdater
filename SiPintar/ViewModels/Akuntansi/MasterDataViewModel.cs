using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.MasterData;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData;


namespace SiPintar.ViewModels.Akuntansi
{
    public class MasterDataViewModel : ViewModelBase
    {
        private readonly MasterDataKeuanganViewModel _masterDataKeuangan;
        private readonly MasterDataAkuntansiViewModel _masterDataAkuntansi;

        public MasterDataViewModel(IRestApiClientModel restApi)
        {
            _masterDataKeuangan = new MasterDataKeuanganViewModel(this, restApi);
            _masterDataAkuntansi = new MasterDataAkuntansiViewModel(this, restApi);
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
                "Akuntansi" => _masterDataAkuntansi,
                "Keuangan" => _masterDataKeuangan,
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
                    case "Akuntansi":
                        ((MasterDataAkuntansiViewModel)PageViewModel).NavigationItems = ((MasterDataAkuntansiViewModel)PageViewModel).GetNavigationItems();
                        ((MasterDataAkuntansiViewModel)PageViewModel).PageViewModel = null!;
                        break;
                    case "Keuangan":
                        ((MasterDataKeuanganViewModel)PageViewModel).NavigationItems = ((MasterDataKeuanganViewModel)PageViewModel).GetNavigationItems();
                        ((MasterDataKeuanganViewModel)PageViewModel).PageViewModel = null!;
                        break;
                    default:
                        break;
                }
            });
        }

        [ExcludeFromCodeCoverage]
        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>();

            Nav.Add(new HorizontalNavigationItem() { Label = "Akuntansi", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Keuangan", IsSelected = false });

            return Nav;
        }
    }
}
