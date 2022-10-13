using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.Bantuan;

namespace SiPintar.ViewModels.Bacameter
{
    public class BantuanViewModel : ViewModelBase
    {
        private readonly CaraPenggunaanViewModel _caraPenggunaan;
        private readonly FaqViewModel _faq;
        private readonly SaranPengaduanViewModel _saranPengaduan;
        public BantuanViewModel(IRestApiClientModel restApi)
        {
            _caraPenggunaan = new CaraPenggunaanViewModel(this);
            _faq = new FaqViewModel(this);
            _saranPengaduan = new SaranPengaduanViewModel(this, restApi);
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private string _currentPageName;
        public string CurrentPageName
        {
            get { return _currentPageName; }
            set { _currentPageName = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Cara Penggunaan" => _caraPenggunaan,
                "Saran & Pengaduan" => _saranPengaduan,
                "FAQ" => _faq,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            CurrentPageName = pageName;

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Cara Penggunaan":
                        ((CaraPenggunaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Saran & Pengaduan":
                        ((SaranPengaduanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "FAQ":
                        ((FaqViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public List<INavigationItem> GetNavigationItems()
        {
            var Nav = new List<INavigationItem>
            {
                new FirstLevelNavigationItem() { Label = "Saran & Pengaduan", Icon = PackIconKind.CommentsText, IsSelected = true },
                new FirstLevelNavigationItem() { Label = "FAQ", Icon = PackIconKind.QuestionAnswer, IsSelected = false },
                //new FirstLevelNavigationItem() { Label = "Cara Penggunaan", Icon = PackIconKind.HowToReg, IsSelected = false },
            };

            return Nav;
        }
    }
}
