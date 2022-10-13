using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.ViewModels.Akuntansi
{
    public class VoucherViewModel : ViewModelBase
    {
        private readonly IsiEditViewModel _isiEdit;
        private readonly PembayaranPembatalanViewModel _pembayaranPembatalan;
        private readonly LembarHarianViewModel _lembarHarian;
        private readonly KuitansiViewModel _kuitansi;

        public VoucherViewModel(IRestApiClientModel restApi)
        {
            _isiEdit = new IsiEditViewModel(this, restApi);
            _pembayaranPembatalan = new PembayaranPembatalanViewModel(this, restApi);
            _lembarHarian = new LembarHarianViewModel(this, restApi);
            _kuitansi = new KuitansiViewModel(this, restApi);
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
                "Isi dan Edit" => _isiEdit,
                "Pembayaran dan Pembatalan" => _pembayaranPembatalan,
                "Lembar Harian" => _lembarHarian,
                "Kuitansi" => _kuitansi,
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
                    case "Isi dan Edit":
                        ((IsiEditViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembayaran dan Pembatalan":
                        ((PembayaranPembatalanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Lembar Harian":
                        ((LembarHarianViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Kuitansi":
                        ((KuitansiViewModel)PageViewModel).OnLoadCommand.Execute(null);
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

            Nav.Add(new HorizontalNavigationItem() { Label = "Isi dan Edit", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Pembayaran dan Pembatalan", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Lembar Harian", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Kuitansi", IsSelected = false });

            return Nav;
        }
    }
}
