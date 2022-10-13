using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.ViewModels.Akuntansi
{
    public class PostingKeuanganViewModel : ViewModelBase
    {
        private readonly PostingTransKasViewModel _postingTransKas;
        private readonly PengeluaranLainnyaViewModel _pengeluaranLainnya;
        private readonly PenerimaanLainnyaViewModel _penerimaanLainnya;
        private readonly ProsesSaldoHarianViewModel _prosesSaldoHarian;
        private readonly ProsesPiutangBulananViewModel _prosesPiutangBulanan;

        public PostingKeuanganViewModel(IRestApiClientModel restApi)
        {
            _postingTransKas = new PostingTransKasViewModel(this, restApi);
            _pengeluaranLainnya = new PengeluaranLainnyaViewModel(this, restApi);
            _penerimaanLainnya = new PenerimaanLainnyaViewModel(this, restApi);
            _prosesSaldoHarian = new ProsesSaldoHarianViewModel(this, restApi);
            _prosesPiutangBulanan = new ProsesPiutangBulananViewModel(this, restApi);
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
                "Posting Trans. Kas" => _postingTransKas,
                "Pengeluaran Lainnya" => _pengeluaranLainnya,
                "Penerimaan Lainnya" => _penerimaanLainnya,
                "Proses Saldo Harian" => _prosesSaldoHarian,
                "Proses Piutang Bulanan" => _prosesPiutangBulanan,
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
                    case "Posting Trans. Kas":
                        ((PostingTransKasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pengeluaran Lainnya":
                        ((PengeluaranLainnyaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Penerimaan Lainnya":
                        ((PenerimaanLainnyaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Proses Saldo Harian":
                        ((ProsesSaldoHarianViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Proses Piutang Bulanan":
                        ((ProsesPiutangBulananViewModel)PageViewModel).OnLoadCommand.Execute(null);
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

            Nav.Add(new HorizontalNavigationItem() { Label = "Posting Trans. Kas", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Pengeluaran Lainnya", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Penerimaan Lainnya", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Proses Saldo Harian", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Proses Piutang Bulanan", IsSelected = false });

            return Nav;
        }
    }
}
