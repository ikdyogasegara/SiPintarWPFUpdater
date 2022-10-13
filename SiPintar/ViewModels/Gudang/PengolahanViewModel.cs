using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.ViewModels.Gudang
{
    public class PengolahanViewModel : ViewModelBase
    {
        private readonly OpnameBarangViewModel _opnameBarangViewModel;
        private readonly PostingViewModel _postingViewModel;
        private readonly PenyesuaianStockViewModel _penyesuaianStockViewModel;
        private readonly StockBarangViewModel _stockBarangHibahViewModel;
        private readonly StockBarangViewModel _stockBarangReturViewModel;

        public PengolahanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _pageViewModel = null!;
            _navigationItems = null!;
            _opnameBarangViewModel = new OpnameBarangViewModel(this, restApi, isTest);
            _postingViewModel = new PostingViewModel(this, restApi, isTest);
            _penyesuaianStockViewModel = new PenyesuaianStockViewModel(this, restApi, isTest);
            _stockBarangHibahViewModel = new StockBarangViewModel(this, restApi, isTest, true);
            _stockBarangReturViewModel = new StockBarangViewModel(this, restApi, isTest, false);
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
                "Opname Barang" => _opnameBarangViewModel,
                "Posting" => _postingViewModel,
                "Penyesuaian Stock" => _penyesuaianStockViewModel,
                "Stock Barang Hibah" => _stockBarangHibahViewModel,
                "Stock Barang Retur" => _stockBarangReturViewModel,
                _ => null!
            };
            LoadPageCommand(pageName);
        }

        public static List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Opname Barang", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Posting", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Penyesuaian Stock", IsSelected = false },
                new HorizontalNavigationItem() {Label = "Stock Barang", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                {
                    new HorizontalNavigationItem() {Label = "Hibah"},
                    new HorizontalNavigationItem() {Label = "Retur"},
                }},
            };
            return nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Opname Barang":
                        {
                            if (PageViewModel is OpnameBarangViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Posting":
                        {
                            if (PageViewModel is PostingViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Penyesuaian Stock":
                        {
                            if (PageViewModel is PenyesuaianStockViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Stock Barang Hibah":
                        {
                            if (PageViewModel is StockBarangViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    case "Stock Barang Retur":
                        {
                            if (PageViewModel is StockBarangViewModel vm)
                            {
                                vm.OnLoadCommand.Execute(null);
                            }
                        }
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
