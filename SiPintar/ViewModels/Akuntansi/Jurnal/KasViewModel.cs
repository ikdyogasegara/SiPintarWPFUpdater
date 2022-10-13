using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.ViewModels.Akuntansi.Jurnal
{
    public class KasViewModel : VmBase
    {
        private readonly PenerimaanKasViewModel _penerimaanKas;
        private readonly PembayaranKasViewModel _pembayaranKas;

        public KasViewModel(JurnalViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            _penerimaanKas = new PenerimaanKasViewModel(this, restApi);
            _pembayaranKas = new PembayaranKasViewModel(this, restApi);
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
                "Penerimaan Kas (JPK)" => _penerimaanKas,
                "Pembayaran Kas (JBK)" => _pembayaranKas,
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
                    case "Penerimaan Kas (JPK)":
                        ((PenerimaanKasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran Kas (JBK)":
                        ((PembayaranKasViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Penerimaan Kas (JPK)", Icon = PackIconKind.CashCheck, IsSelected = false, IconForeground = "#126E3D"},
                new ColoredFirstLevelNavigationItem() { Label = "Pembayaran Kas (JBK)", Icon = PackIconKind.CashFast, IsSelected = false, IconForeground = "#C30C0C"}
            };

            return Nav;
        }
    }
}
