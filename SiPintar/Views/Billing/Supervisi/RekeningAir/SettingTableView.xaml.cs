using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    /// <summary>
    /// Interaction logic for SettingTableView.xaml
    /// </summary>
    public partial class SettingTableView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        public SettingTableView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;

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
                { "FlagPublish", Publish.IsChecked },
                { "FlagVerifikasi", Verifikasi.IsChecked },
                { "Nosamb", Nosamb.IsChecked },
                { "Nama", Nama.IsChecked },
                { "NamaGolongan", NamaGolongan.IsChecked },
                { "NamaDiameter", NamaDiameter.IsChecked },
                { "KodeCabang", KodeCabang.IsChecked },
                { "NamaCabang", NamaCabang.IsChecked },
                { "KodeRayon", KodeRayon.IsChecked },
                { "NamaRayon", NamaRayon.IsChecked },
                { "KodeKelurahan", KodeKelurahan.IsChecked },
                { "NamaKelurahan", NamaKelurahan.IsChecked },
                { "KodeWilayah", KodeWilayah.IsChecked },
                { "NamaWilayah", NamaWilayah.IsChecked },
                { "IdFlag", Flag.IsChecked },
                { "NamaFlag", NamaFlag.IsChecked },
                { "Kelainan", Kelainan.IsChecked },
                { "Taksasi", Taksasi.IsChecked },
                { "PetugasBaca", PetugasBaca.IsChecked },
                { "KodeKolektif", KodeKolektif.IsChecked },
                { "NamaKolektif", NamaKolektif.IsChecked },
                { "PemeliharaanLain", PemeliharaanLain.IsChecked },
                { "AdministrasiLain", AdministrasiLain.IsChecked },
                { "RetribusiLain", RetribusiLain.IsChecked },
                { "NoRekening", NoRekening.IsChecked },
                { "StanLalu", SambunganLalu.IsChecked },
                { "StanSkrg", SambunganKini.IsChecked },
                { "StanAngkat", SambunganAngkat.IsChecked },
                { "Pakai", Pakai.IsChecked },
                { "NamaStatus", Status.IsChecked },
                { "PakaiHitung", PakaiHitung.IsChecked },
                { "BiayaPemakaian", BiayaPakai.IsChecked },
                { "Administrasi", Administrasi.IsChecked },
                { "Pemeliharaan", Pemeliharaan.IsChecked },
                { "Retribusi", Retribusi.IsChecked },
                { "Meterai", Materai.IsChecked },
                { "Rekair", RekeningAir.IsChecked },
                { "FlagKoreksi", Koreksi.IsChecked },
                { "KodeGolongan", KodeGolongan.IsChecked },
                { "KodeDiameter", KodeDiameter.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "NamaUser", Kasir.IsChecked },
                { "NamaLoket", LoketBayar.IsChecked },
                { "WaktuTransaksi", TanggalBayar.IsChecked },
                { "Pelayanan", Pelayanan.IsChecked },
                { "AirLimbah", AirLimbah.IsChecked },
                { "DendaPakai0", DendaPakai0.IsChecked },
                { "Denda", Denda.IsChecked },
                { "Total", Total.IsChecked },
                { "WaktuKoreksi", TanggalKoreksi.IsChecked },
                { "WaktuPublish", TanggalPublish.IsChecked },
                { "FlagUpload", Upload.IsChecked },
                { "Ppn", Ppn.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            var isSelected = false;

            if (Publish.IsChecked == true)
                isSelected = true;
            if (Verifikasi.IsChecked == true)
                isSelected = true;
            if (Nosamb.IsChecked == true)
                isSelected = true;
            if (Nama.IsChecked == true)
                isSelected = true;
            if (NamaGolongan.IsChecked == true)
                isSelected = true;
            if (NamaDiameter.IsChecked == true)
                isSelected = true;
            if (KodeCabang.IsChecked == true)
                isSelected = true;
            if (NamaCabang.IsChecked == true)
                isSelected = true;
            if (KodeRayon.IsChecked == true)
                isSelected = true;
            if (NamaRayon.IsChecked == true)
                isSelected = true;
            if (KodeKelurahan.IsChecked == true)
                isSelected = true;
            if (NamaKelurahan.IsChecked == true)
                isSelected = true;
            if (KodeWilayah.IsChecked == true)
                isSelected = true;
            if (NamaWilayah.IsChecked == true)
                isSelected = true;
            if (Flag.IsChecked == true)
                isSelected = true;
            if (NamaFlag.IsChecked == true)
                isSelected = true;
            if (Kelainan.IsChecked == true)
                isSelected = true;
            if (Taksasi.IsChecked == true)
                isSelected = true;
            if (PetugasBaca.IsChecked == true)
                isSelected = true;
            if (KodeKolektif.IsChecked == true)
                isSelected = true;
            if (NamaKolektif.IsChecked == true)
                isSelected = true;
            if (PemeliharaanLain.IsChecked == true)
                isSelected = true;
            if (AdministrasiLain.IsChecked == true)
                isSelected = true;
            if (RetribusiLain.IsChecked == true)
                isSelected = true;
            if (NoRekening.IsChecked == true)
                isSelected = true;
            if (SambunganLalu.IsChecked == true)
                isSelected = true;
            if (SambunganKini.IsChecked == true)
                isSelected = true;
            if (SambunganAngkat.IsChecked == true)
                isSelected = true;
            if (Pakai.IsChecked == true)
                isSelected = true;
            if (Status.IsChecked == true)
                isSelected = true;
            if (PakaiHitung.IsChecked == true)
                isSelected = true;
            if (BiayaPakai.IsChecked == true)
                isSelected = true;
            if (Administrasi.IsChecked == true)
                isSelected = true;
            if (Pemeliharaan.IsChecked == true)
                isSelected = true;
            if (Retribusi.IsChecked == true)
                isSelected = true;
            if (Materai.IsChecked == true)
                isSelected = true;
            if (RekeningAir.IsChecked == true)
                isSelected = true;
            if (Koreksi.IsChecked == true)
                isSelected = true;
            if (KodeGolongan.IsChecked == true)
                isSelected = true;
            if (KodeDiameter.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (Kasir.IsChecked == true)
                isSelected = true;
            if (LoketBayar.IsChecked == true)
                isSelected = true;
            if (TanggalBayar.IsChecked == true)
                isSelected = true;
            if (Pelayanan.IsChecked == true)
                isSelected = true;
            if (AirLimbah.IsChecked == true)
                isSelected = true;
            if (DendaPakai0.IsChecked == true)
                isSelected = true;
            if (Denda.IsChecked == true)
                isSelected = true;
            if (Total.IsChecked == true)
                isSelected = true;
            if (TanggalKoreksi.IsChecked == true)
                isSelected = true;
            if (TanggalPublish.IsChecked == true)
                isSelected = true;
            if (Upload.IsChecked == true)
                isSelected = true;
            if (Ppn.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Publish.IsChecked = true;
            Verifikasi.IsChecked = true;
            Nosamb.IsChecked = true;
            Nama.IsChecked = true;
            NamaGolongan.IsChecked = true;
            NamaDiameter.IsChecked = true;
            KodeCabang.IsChecked = true;
            NamaCabang.IsChecked = true;
            KodeRayon.IsChecked = true;
            NamaRayon.IsChecked = true;
            KodeKelurahan.IsChecked = true;
            NamaKelurahan.IsChecked = true;
            KodeWilayah.IsChecked = true;
            NamaWilayah.IsChecked = true;
            Flag.IsChecked = true;
            NamaFlag.IsChecked = true;
            Kelainan.IsChecked = true;
            Taksasi.IsChecked = true;
            PetugasBaca.IsChecked = true;
            KodeKolektif.IsChecked = true;
            NamaKolektif.IsChecked = true;
            PemeliharaanLain.IsChecked = true;
            AdministrasiLain.IsChecked = true;
            RetribusiLain.IsChecked = true;
            NoRekening.IsChecked = true;
            SambunganLalu.IsChecked = true;
            SambunganKini.IsChecked = true;
            SambunganAngkat.IsChecked = true;
            Pakai.IsChecked = true;
            Status.IsChecked = true;
            PakaiHitung.IsChecked = true;
            BiayaPakai.IsChecked = true;
            Administrasi.IsChecked = true;
            Pemeliharaan.IsChecked = true;
            Retribusi.IsChecked = true;
            Materai.IsChecked = true;
            RekeningAir.IsChecked = true;
            Koreksi.IsChecked = true;
            KodeGolongan.IsChecked = true;
            KodeDiameter.IsChecked = true;
            Alamat.IsChecked = true;
            Kasir.IsChecked = true;
            LoketBayar.IsChecked = true;
            TanggalBayar.IsChecked = true;
            Pelayanan.IsChecked = true;
            AirLimbah.IsChecked = true;
            DendaPakai0.IsChecked = true;
            Denda.IsChecked = true;
            Total.IsChecked = true;
            TanggalKoreksi.IsChecked = true;
            TanggalPublish.IsChecked = true;
            Upload.IsChecked = true;
            Ppn.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Publish.IsChecked = false;
            Verifikasi.IsChecked = false;
            Nosamb.IsChecked = false;
            Nama.IsChecked = false;
            NamaGolongan.IsChecked = false;
            NamaDiameter.IsChecked = false;
            KodeCabang.IsChecked = false;
            NamaCabang.IsChecked = false;
            KodeRayon.IsChecked = false;
            NamaRayon.IsChecked = false;
            KodeKelurahan.IsChecked = false;
            NamaKelurahan.IsChecked = false;
            KodeWilayah.IsChecked = false;
            NamaWilayah.IsChecked = false;
            Flag.IsChecked = false;
            NamaFlag.IsChecked = false;
            Kelainan.IsChecked = false;
            Taksasi.IsChecked = false;
            PetugasBaca.IsChecked = false;
            KodeKolektif.IsChecked = false;
            NamaKolektif.IsChecked = false;
            PemeliharaanLain.IsChecked = false;
            AdministrasiLain.IsChecked = false;
            RetribusiLain.IsChecked = false;
            NoRekening.IsChecked = false;
            SambunganLalu.IsChecked = false;
            SambunganKini.IsChecked = false;
            SambunganAngkat.IsChecked = false;
            Pakai.IsChecked = false;
            Status.IsChecked = false;
            PakaiHitung.IsChecked = false;
            BiayaPakai.IsChecked = false;
            Administrasi.IsChecked = false;
            Pemeliharaan.IsChecked = false;
            Retribusi.IsChecked = false;
            Materai.IsChecked = false;
            RekeningAir.IsChecked = false;
            Koreksi.IsChecked = false;
            KodeGolongan.IsChecked = false;
            KodeDiameter.IsChecked = false;
            Alamat.IsChecked = false;
            Kasir.IsChecked = false;
            LoketBayar.IsChecked = false;
            TanggalBayar.IsChecked = false;
            Pelayanan.IsChecked = false;
            AirLimbah.IsChecked = false;
            DendaPakai0.IsChecked = false;
            Denda.IsChecked = false;
            Total.IsChecked = false;
            TanggalKoreksi.IsChecked = false;
            TanggalPublish.IsChecked = false;
            Upload.IsChecked = false;
            Ppn.IsChecked = false;
        }
    }
}
