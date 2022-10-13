using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;


namespace SiPintar.ViewModels.Akuntansi.MasterData
{
    [ExcludeFromCodeCoverage]
    public class MasterDataAkuntansiViewModel : ViewModelBase
    {
        private readonly KelompokKodePerkiraanViewModel _kelompokKodePerkiraan;
        private readonly InteraksiViewModel _interaksi;
        private readonly SaldoAwalPerkiraanViewModel _saldoAwalPerkiraan;
        private readonly AnggaranLabaRugiViewModel _anggaranLabaRugi;
        private readonly AnggaranPerputaranUangViewModel _anggaranPerputaranUang;
        private readonly PersentasePenyusutanAktivaViewModel _persentasePenyusutanAktiva;
        private readonly PengelompokanAktivaViewModel _pengelompokanAktiva;

        public MasterDataAkuntansiViewModel(MasterDataViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _kelompokKodePerkiraan = new KelompokKodePerkiraanViewModel(this, restApi);
            _interaksi = new InteraksiViewModel(this, restApi);

            _saldoAwalPerkiraan = new SaldoAwalPerkiraanViewModel(this, restApi);
            _anggaranLabaRugi = new AnggaranLabaRugiViewModel(this, restApi);
            _anggaranPerputaranUang = new AnggaranPerputaranUangViewModel(this, restApi);
            _persentasePenyusutanAktiva = new PersentasePenyusutanAktivaViewModel(this, restApi);
            _pengelompokanAktiva = new PengelompokanAktivaViewModel(this, restApi);

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

                "Kelompok Kode Perkiraan(XX)" => _kelompokKodePerkiraan,
                "Kelompok Kode Perkiraan(XX,YY)" => _kelompokKodePerkiraan,
                "Kelompok Kode Perkiraan(XX,YY,ZZ)" => _kelompokKodePerkiraan,

                "Interaksi Layanan & Kd. Perkiraan (Air/Non)" => _interaksi,
                "Inter. Jenis Persediaan Kode Perkiraan" => _interaksi,
                "Inter. Penyusutan & Kode Perkiraan Biaya" => _interaksi,

                "Saldo Awal Perkiraan" => _saldoAwalPerkiraan,
                "Anggaran Laba-Rugi" => _anggaranLabaRugi,
                "Anggaran Perputaran Uang (Detail)" => _anggaranPerputaranUang,
                "Anggaran Perputaran Uang (Rekap)" => _anggaranPerputaranUang,
                "Persentase Penyusutan Aktiva" => _persentasePenyusutanAktiva,
                "Pengelompokan Aktiva" => _pengelompokanAktiva,

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
                    case "Kelompok Kode Perkiraan(XX)":
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Kelompok Kode Perkiraan(XX)");
                        break;
                    case "Kelompok Kode Perkiraan(XX,YY)":
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Kelompok Kode Perkiraan(XX,YY)");
                        break;
                    case "Kelompok Kode Perkiraan(XX,YY,ZZ)":
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Kelompok Kode Perkiraan(XX,YY,ZZ)");
                        break;

                    case "Interaksi Layanan & Kd. Perkiraan (Air/Non)":
                        _ = Task.Run(() =>
                        {
                            //((AsyncCommandBase)((InteraksiViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Interaksi Layanan & Kd. Perkiraan (Air/Non)");
                            ((InteraksiViewModel)PageViewModel).OnLoadCommand.Execute("Interaksi Layanan & Kd. Perkiraan (Air/Non)");
                        });
                        break;
                    case "Inter. Jenis Persediaan Kode Perkiraan":
                        _ = Task.Run(() =>
                        {
                            ((InteraksiViewModel)PageViewModel).OnLoadCommand.Execute("Inter. Jenis Persediaan Kode Perkiraan");
                        });
                        break;
                    case "Inter. Penyusutan & Kode Perkiraan Biaya":
                        _ = ((AsyncCommandBase)((InteraksiViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Inter. Penyusutan & Kode Perkiraan Biaya");
                        break;
                    case "Saldo Awal Perkiraan":
                        _ = ((AsyncCommandBase)((SaldoAwalPerkiraanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                        break;
                    case "Anggaran Laba-Rugi":
                        _ = ((AsyncCommandBase)((AnggaranLabaRugiViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                        break;
                    case "Anggaran Perputaran Uang (Detail)":
                        _ = ((AsyncCommandBase)((AnggaranPerputaranUangViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Detail");
                        break;
                    case "Anggaran Perputaran Uang (Rekap)":
                        _ = ((AsyncCommandBase)((AnggaranPerputaranUangViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Rekap");
                        break;
                    case "Persentase Penyusutan Aktiva":
                        _ = ((AsyncCommandBase)((PersentasePenyusutanAktivaViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                        break;
                    case "Pengelompokan Aktiva":
                        _ = ((AsyncCommandBase)((PengelompokanAktivaViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
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
                new ColoredFirstLevelNavigationItem() { Label = "Kelompok Kode Perkiraan(XX)", Icon = PackIconKind.CodeParentheses, IsSelected = false, IconForeground = "#3CB1F2"},
                new ColoredFirstLevelNavigationItem() { Label = "Kelompok Kode Perkiraan(XX,YY)", Icon = PackIconKind.CodeParenthesesBox, IsSelected = false, IconForeground = "#028DDB"},
                new ColoredFirstLevelNavigationItem() { Label = "Kelompok Kode Perkiraan(XX,YY,ZZ)", Icon = PackIconKind.ApplicationParentheses, IsSelected = false, IconForeground = "#026AB5"},
                new ColoredFirstLevelNavigationItem() { Label = "Interaksi Layanan & Kd. Perkiraan (Air/Non)", Icon = PackIconKind.RoomService, IsSelected = false, IconForeground = "#338D5D"},
                new ColoredFirstLevelNavigationItem() { Label = "Inter. Jenis Persediaan Kode Perkiraan", Icon = PackIconKind.Warehouse, IsSelected = false, IconForeground = "#A66335"},
                new ColoredFirstLevelNavigationItem() { Label = "Inter. Penyusutan & Kode Perkiraan Biaya", Icon = PackIconKind.ArrowCollapseHorizontal, IsSelected = false, IconForeground = "#D78813"},
                new ColoredFirstLevelNavigationItem() { Label = "Saldo Awal Perkiraan", Icon = PackIconKind.CreditCardScan, IsSelected = false, IconForeground = "#A51D16"},
                new ColoredFirstLevelNavigationItem() { Label = "Anggaran Laba-Rugi", Icon = PackIconKind.FileChartOutline, IsSelected = false, IconForeground = "#FC6740"},
                new ColoredFirstLevelNavigationItem() { Label = "Anggaran Perputaran Uang (Detail)", Icon = PackIconKind.ChartArc, IsSelected = false, IconForeground = "#9D68B7"},
                new ColoredFirstLevelNavigationItem() { Label = "Anggaran Perputaran Uang (Rekap)", Icon = PackIconKind.ChartArc, IsSelected = false, IconForeground = "#9D68B7"},
                new ColoredFirstLevelNavigationItem() { Label = "Persentase Penyusutan Aktiva", Icon = PackIconKind.Percent, IsSelected = false, IconForeground = "#C30C0C"},
                new ColoredFirstLevelNavigationItem() { Label = "Pengelompokan Aktiva", Icon = PackIconKind.HomeCircle, IsSelected = false, IconForeground = "#126E3D"},
            };

            return Nav;
        }
    }
}
