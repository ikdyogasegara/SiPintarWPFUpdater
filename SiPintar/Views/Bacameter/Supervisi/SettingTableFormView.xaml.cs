using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "IdPelanggan", IdPelanggan.IsChecked },
                { "NamaPelanggan", NamaPelanggan.IsChecked },
                { "Status", Status.IsChecked },
                { "Verifikasi", Verifikasi.IsChecked },
                { "StanAwal", StanAwal.IsChecked },
                { "StanAkhir", StanAkhir.IsChecked },
                { "Pakai", Pakai.IsChecked },
                { "StanAngkat", StanAngkat.IsChecked },
                { "BiayaPemakaian", BiayaPemakaian.IsChecked },
                { "Administrasi", Administrasi.IsChecked },
                { "Pemeliharaan", Pemeliharaan.IsChecked },
                { "Retribusi", Retribusi.IsChecked },
                { "Pelayanan", Pelayanan.IsChecked },
                { "AirLimbah", AirLimbah.IsChecked },
                { "DendaPakai0", DendaPakai0.IsChecked },
                { "AdministrasiLain", AdministrasiLain.IsChecked },
                { "PemeliharaanLain", PemeliharaanLain.IsChecked },
                { "RetribusiLain", RetribusiLain.IsChecked },
                { "Ppn", Ppn.IsChecked },
                { "Meterai", Meterai.IsChecked },
                { "Rekair", Rekair.IsChecked },
                { "Denda", Denda.IsChecked },
                { "Total", Total.IsChecked },
                { "MerekMeter", MerekMeter.IsChecked },
                { "KodeRayon", KodeRayon.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "NoSambungan", NoSambungan.IsChecked },
                { "KodeGolongan", KodeGolongan.IsChecked },
                { "Golongan", Golongan.IsChecked },
                { "Diameter", Diameter.IsChecked },
                { "Kelainan", Kelainan.IsChecked },
                { "SumberModul", SumberModul.IsChecked },
                { "NamaPetugas", NamaPetugas.IsChecked },
                { "WaktuVerifikasi", WaktuVerifikasi.IsChecked },
                { "WaktuBaca", WaktuBaca.IsChecked },
                { "Lampiran", Lampiran.IsChecked },
                { "WideRowHeight", false },
                { "NormalRowHeight", false },
                { "NarrowRowHeight", true }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (IdPelanggan.IsChecked == true)
                IsSelected = true;
            if (NamaPelanggan.IsChecked == true)
                IsSelected = true;
            if (Status.IsChecked == true)
                IsSelected = true;
            if (Verifikasi.IsChecked == true)
                IsSelected = true;
            if (StanAwal.IsChecked == true)
                IsSelected = true;
            if (StanAkhir.IsChecked == true)
                IsSelected = true;
            if (Pakai.IsChecked == true)
                IsSelected = true;
            if (StanAngkat.IsChecked == true)
                IsSelected = true;
            if (BiayaPemakaian.IsChecked == true)
                IsSelected = true;
            if (Administrasi.IsChecked == true)
                IsSelected = true;
            if (Pemeliharaan.IsChecked == true)
                IsSelected = true;
            if (Retribusi.IsChecked == true)
                IsSelected = true;
            if (Pelayanan.IsChecked == true)
                IsSelected = true;
            if (AirLimbah.IsChecked == true)
                IsSelected = true;
            if (DendaPakai0.IsChecked == true)
                IsSelected = true;
            if (AdministrasiLain.IsChecked == true)
                IsSelected = true;
            if (PemeliharaanLain.IsChecked == true)
                IsSelected = true;
            if (RetribusiLain.IsChecked == true)
                IsSelected = true;
            if (Ppn.IsChecked == true)
                IsSelected = true;
            if (Meterai.IsChecked == true)
                IsSelected = true;
            if (Rekair.IsChecked == true)
                IsSelected = true;
            if (Denda.IsChecked == true)
                IsSelected = true;
            if (Total.IsChecked == true)
                IsSelected = true;
            if (MerekMeter.IsChecked == true)
                IsSelected = true;
            if (KodeRayon.IsChecked == true)
                IsSelected = true;
            if (Rayon.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (NoSambungan.IsChecked == true)
                IsSelected = true;
            if (KodeGolongan.IsChecked == true)
                IsSelected = true;
            if (Golongan.IsChecked == true)
                IsSelected = true;
            if (Diameter.IsChecked == true)
                IsSelected = true;
            if (Kelainan.IsChecked == true)
                IsSelected = true;
            if (SumberModul.IsChecked == true)
                IsSelected = true;
            if (NamaPetugas.IsChecked == true)
                IsSelected = true;
            if (WaktuVerifikasi.IsChecked == true)
                IsSelected = true;
            if (WaktuBaca.IsChecked == true)
                IsSelected = true;
            if (Lampiran.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            IdPelanggan.IsChecked = true;
            NamaPelanggan.IsChecked = true;
            Status.IsChecked = true;
            Verifikasi.IsChecked = true;
            StanAwal.IsChecked = true;
            StanAkhir.IsChecked = true;
            Pakai.IsChecked = true;
            StanAngkat.IsChecked = true;
            BiayaPemakaian.IsChecked = true;
            Administrasi.IsChecked = true;
            Pemeliharaan.IsChecked = true;
            Retribusi.IsChecked = true;
            Pelayanan.IsChecked = true;
            AirLimbah.IsChecked = true;
            DendaPakai0.IsChecked = true;
            AdministrasiLain.IsChecked = true;
            PemeliharaanLain.IsChecked = true;
            RetribusiLain.IsChecked = true;
            Ppn.IsChecked = true;
            Meterai.IsChecked = true;
            Rekair.IsChecked = true;
            Denda.IsChecked = true;
            Total.IsChecked = true;
            MerekMeter.IsChecked = true;
            KodeRayon.IsChecked = true;
            Rayon.IsChecked = true;
            Alamat.IsChecked = true;
            NoSambungan.IsChecked = true;
            KodeGolongan.IsChecked = true;
            Golongan.IsChecked = true;
            Diameter.IsChecked = true;
            Kelainan.IsChecked = true;
            SumberModul.IsChecked = true;
            NamaPetugas.IsChecked = true;
            WaktuVerifikasi.IsChecked = true;
            WaktuBaca.IsChecked = true;
            Lampiran.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            IdPelanggan.IsChecked = false;
            NamaPelanggan.IsChecked = false;
            Status.IsChecked = false;
            Verifikasi.IsChecked = false;
            StanAwal.IsChecked = false;
            StanAkhir.IsChecked = false;
            Pakai.IsChecked = false;
            StanAngkat.IsChecked = false;
            BiayaPemakaian.IsChecked = false;
            Administrasi.IsChecked = false;
            Pemeliharaan.IsChecked = false;
            Retribusi.IsChecked = false;
            Pelayanan.IsChecked = false;
            AirLimbah.IsChecked = false;
            DendaPakai0.IsChecked = false;
            AdministrasiLain.IsChecked = false;
            PemeliharaanLain.IsChecked = false;
            RetribusiLain.IsChecked = false;
            Ppn.IsChecked = false;
            Meterai.IsChecked = false;
            Rekair.IsChecked = false;
            Denda.IsChecked = false;
            Total.IsChecked = false;
            MerekMeter.IsChecked = false;
            KodeRayon.IsChecked = false;
            Rayon.IsChecked = false;
            Alamat.IsChecked = false;
            NoSambungan.IsChecked = false;
            KodeGolongan.IsChecked = false;
            Golongan.IsChecked = false;
            Diameter.IsChecked = false;
            Kelainan.IsChecked = false;
            SumberModul.IsChecked = false;
            NamaPetugas.IsChecked = false;
            WaktuVerifikasi.IsChecked = false;
            WaktuBaca.IsChecked = false;
            Lampiran.IsChecked = false;
        }
    }
}
