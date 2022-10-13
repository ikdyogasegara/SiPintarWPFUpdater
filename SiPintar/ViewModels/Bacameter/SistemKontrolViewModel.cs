using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.ViewModels.Bacameter
{
    public class SistemKontrolViewModel : ViewModelBase
    {
        private readonly RuteBacaMeterViewModel _ruteBacaMeter;
        private readonly WilayahAdministrasiViewModel _wilayahAdministrasi;
        private readonly TarifGolonganViewModel _tarifGolongan;
        private readonly PetugasBacaViewModel _petugasBaca;
        private readonly DaftarKelainanViewModel _daftarKelainan;
        private readonly DataPelangganViewModel _dataPelanggan;
        private readonly DistribusiPelangganViewModel _distribusiPelanggan;
        private readonly JadwalRuteBacaViewModel _jadwalRuteBaca;
        private readonly DataPembacaanViewModel _dataPembacaan;
        private readonly SmsGatewayViewModel _smsGateway;
        private readonly PenggunaHakAksesViewModel _penggunaHakAkses;
        private readonly PengaturanUmumViewModel _pengaturanUmum;
        private readonly PengaturanPutstampViewModel _pengaturanPutstamp;
        private readonly SinkronisasiViewModel _sinkronisasi;
        private readonly PerawatanDatabaseViewModel _perawatanDatabase;
        private readonly LogAksesUserViewModel _logAksesUser;

        public SistemKontrolViewModel(IRestApiClientModel restApi)
        {
            _ruteBacaMeter = new RuteBacaMeterViewModel(restApi);
            _wilayahAdministrasi = new WilayahAdministrasiViewModel(restApi);
            _tarifGolongan = new TarifGolonganViewModel(restApi);
            _petugasBaca = new PetugasBacaViewModel(restApi);
            _daftarKelainan = new DaftarKelainanViewModel(restApi);
            _dataPelanggan = new DataPelangganViewModel(restApi);
            _distribusiPelanggan = new DistribusiPelangganViewModel(restApi);
            _jadwalRuteBaca = new JadwalRuteBacaViewModel(restApi);
            _dataPembacaan = new DataPembacaanViewModel(restApi);
            _smsGateway = new SmsGatewayViewModel(restApi);
            _penggunaHakAkses = new PenggunaHakAksesViewModel(restApi);
            _pengaturanUmum = new PengaturanUmumViewModel(restApi);
            _pengaturanPutstamp = new PengaturanPutstampViewModel(restApi);
            _sinkronisasi = new SinkronisasiViewModel(restApi);
            _perawatanDatabase = new PerawatanDatabaseViewModel(restApi);
            _logAksesUser = new LogAksesUserViewModel(restApi);
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
                "Rute Baca Meter" => _ruteBacaMeter,
                "Wilayah Administrasi" => _wilayahAdministrasi,
                "Tarif & Golongan" => _tarifGolongan,
                "Petugas Baca" => _petugasBaca,
                "Daftar Kelainan" => _daftarKelainan,
                "Data Pelanggan" => _dataPelanggan,
                "Distribusi Pelanggan" => _distribusiPelanggan,
                "Jadwal Rute Baca" => _jadwalRuteBaca,
                "Data Pembacaan" => _dataPembacaan,
                "SMS Gateway" => _smsGateway,
                "Pengguna & Hak Akses" => _penggunaHakAkses,
                "Pengaturan Umum" => _pengaturanUmum,
                "Pengaturan Put Stamp" => _pengaturanPutstamp,
                "Sinkronisasi" => _sinkronisasi,
                "Perawatan Database" => _perawatanDatabase,
                "Log Akses User" => _logAksesUser,
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
                    case "Rute Baca Meter":
                        ((RuteBacaMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Wilayah Administrasi":
                        ((WilayahAdministrasiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tarif & Golongan":
                        ((TarifGolonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Petugas Baca":
                        ((PetugasBacaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Daftar Kelainan":
                        ((DaftarKelainanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Data Pelanggan":
                        ((DataPelangganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Distribusi Pelanggan":
                        ((DistribusiPelangganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Jadwal Rute Baca":
                        ((JadwalRuteBacaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Data Pembacaan":
                        ((DataPembacaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "SMS Gateway":
                        ((SmsGatewayViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pengguna & Hak Akses":
                        ((PenggunaHakAksesViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pengaturan Umum":
                        ((PengaturanUmumViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pengaturan Put Stamp":
                        ((PengaturanPutstampViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Sinkronisasi":
                        ((SinkronisasiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Perawatan Database":
                        ((PerawatanDatabaseViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Log Akses User":
                        ((LogAksesUserViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                new SubheaderNavigationItem() { Subheader = "Master Data Base" },
                new FirstLevelNavigationItem() { Label = "Rute Baca Meter", Icon = PackIconKind.Routes, IsSelected = true },
                new FirstLevelNavigationItem() { Label = "Wilayah Administrasi", Icon = PackIconKind.Location, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Tarif & Golongan", Icon = PackIconKind.Diameter, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Petugas Baca", Icon = PackIconKind.AccountFilter, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Daftar Kelainan", Icon = PackIconKind.Warning, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Data Pelanggan", Icon = PackIconKind.Accounts, IsSelected = false },

                new SubheaderNavigationItem() { Subheader = "Laporan" },
                new FirstLevelNavigationItem() { Label = "Distribusi Pelanggan", Icon = PackIconKind.DistributeHorizontalRight, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Jadwal Rute Baca", Icon = PackIconKind.Schedule, IsSelected = false },

                new SubheaderNavigationItem() { Subheader = "Atur Sistem" },
                new FirstLevelNavigationItem() { Label = "Data Pembacaan", Icon = PackIconKind.Book, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "SMS Gateway", Icon = PackIconKind.MobilePhone, IsSelected = false },
                //new FirstLevelNavigationItem() { Label = "Pengguna & Hak Akses", Icon = PackIconKind.Accounts, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pengaturan Umum", Icon = PackIconKind.Gear, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Pengaturan Put Stamp", Icon = PackIconKind.Settings, IsSelected = false },
                //new FirstLevelNavigationItem() { Label = "Sinkronisasi", Icon = PackIconKind.Sync, IsSelected = false },

                new SubheaderNavigationItem() { Subheader = "Lain-Lain" },
                //new FirstLevelNavigationItem() { Label = "Perawatan Database", Icon = PackIconKind.Database, IsSelected = false },
                new FirstLevelNavigationItem() { Label = "Log Akses User", Icon = PackIconKind.BookAccount, IsSelected = false },
            };

            return Nav;
        }
    }
}
