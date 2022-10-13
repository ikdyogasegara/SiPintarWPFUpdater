using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.ViewModels.Akuntansi.MasterData
{
    public class MasterDataKeuanganViewModel : VmBase
    {
        private readonly AnggaranPenagihanBulananViewModel _anggaranPenagihanBulanan;
        private readonly PersentaseTarifPajakViewModel _persentaseTarifPajak;
        private readonly SaldoAwalViewModel _saldoAwal;

        public MasterDataKeuanganViewModel(MasterDataViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            _anggaranPenagihanBulanan = new AnggaranPenagihanBulananViewModel(this, restApi);
            _persentaseTarifPajak = new PersentaseTarifPajakViewModel(this, restApi);
            _saldoAwal = new SaldoAwalViewModel(this, restApi);
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
                "Saldo Awal / Rekon. Kas & Bank" => _saldoAwal,
                "Persentase Tarif Pajak" => _persentaseTarifPajak,
                "Anggaran Penagihan Bulanan" => _anggaranPenagihanBulanan,

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
                    case "Saldo Awal / Rekon. Kas & Bank":
                        ((SaldoAwalViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Persentase Tarif Pajak":
                        ((PersentaseTarifPajakViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Anggaran Penagihan Bulanan":
                        ((AnggaranPenagihanBulananViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Saldo Awal / Rekon. Kas & Bank", Icon = PackIconKind.ScaleUnbalanced, IsSelected = false, IconForeground = "#3CB1F2"},
                new ColoredFirstLevelNavigationItem() { Label = "Persentase Tarif Pajak", Icon = PackIconKind.LabelPercent, IsSelected = false, IconForeground = "#B86434"},
                new ColoredFirstLevelNavigationItem() { Label = "Anggaran Penagihan Bulanan", Icon = PackIconKind.Abacus, IsSelected = false, IconForeground = "#338D5D"}
            };

            return Nav;
        }
    }

}
