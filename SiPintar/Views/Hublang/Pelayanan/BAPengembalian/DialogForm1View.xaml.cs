using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.BAPengembalian
{
    /// <summary>
    /// Interaction logic for DialogForm1View.xaml
    /// </summary>
    public partial class DialogForm1View : UserControl
    {
        private readonly BaPengembalianViewModel ViewModel;

        public DialogForm1View(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as BaPengembalianViewModel;
            DataContext = ViewModel;
            Title.Text = $"{(!ViewModel.IsAdd ? "Koreksi" : "Tambah")} Berita Acara Pengembalian";

            SetupPeriodeDRD();

            if (!ViewModel.IsAdd)
            {
                ViewModel.SelectedKondisiMeter = ViewModel.KondisiMeterList.FirstOrDefault(c => c.IdKondisiMeter == ViewModel.SelectedData.IdKondisiMeter);
                ViewModel.SelectedMasterPegawai = ViewModel.MasterPegawaiList.FirstOrDefault(c => c.IdPegawai == ViewModel.SelectedData.IdUser);
                ViewModel.SelectedPeriodeTransaksi = ViewModel.PeriodeTransaksiList.FirstOrDefault(x => x.NamaPeriode == ViewModel.SelectedData.NamaPeriode);
            }
            else
            {
                StanLalu.Text = "-";
                StanKini.Text = "-";
                StanAngkat.Text = "-";
                Pakai.Text = "-";
                BiayaPemakaian.Text = "-";
                Administrasi.Text = "-";
                AdministrasiLain.Text = "-";
                Pemeliharaan.Text = "-";
                PemeliharaanLain.Text = "-";
                Retribusi.Text = "-";
                RetribusiLain.Text = "-";
                Materai.Text = "-";
                Tagihan.Text = "-";
                StanLalu2.Text = "-";
                StanKini2.Text = "-";
                StanAngkat2.Text = "-";
                Pakai2.Text = "-";
                BiayaPemakaian2.Text = "-";
                Administrasi2.Text = "-";
                AdministrasiLain2.Text = "-";
                Pemeliharaan2.Text = "-";
                PemeliharaanLain2.Text = "-";
                Retribusi2.Text = "-";
                RetribusiLain2.Text = "-";
                Materai2.Text = "-";
                Tagihan2.Text = "-";

            }

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void SetupPeriodeDRD()
        {
            if (ViewModel.IsAdd != false)
                BulanDRD.IsEnabled = true;
            else
                BulanDRD.IsEnabled = false;
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void BulanDRD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();

        }

        private void NewStanLalu_TextChanged(object sender, TextChangedEventArgs e)
        {
            validasi();
        }


        private void CheckButton()
        {
            BtnSubmit.IsEnabled = true;
            WarningStan.Visibility = Visibility.Collapsed;
            if (string.IsNullOrWhiteSpace(BulanDRD.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Alasan.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Petugas.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(KondisiMeter.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(NewStanLalu.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(NewStanKini.Text))
                BtnSubmit.IsEnabled = false;
            if (DecimalValidationHelper.FormatStringIdToDecimal(NewStanKini.Text) < DecimalValidationHelper.FormatStringIdToDecimal(NewStanLalu.Text))
            {
                WarningStan.Visibility = Visibility.Visible;
                BtnSubmit.IsEnabled = false;
            }
        }

        private void Text_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            CheckButton();
        }

        public void validasi()
        {
            CheckButton();

            decimal StanLalu = DecimalValidationHelper.FormatStringIdToDecimal(NewStanLalu.Text);
            decimal StanKini = DecimalValidationHelper.FormatStringIdToDecimal(NewStanKini.Text);

            var param = new Dictionary<string, dynamic>();
            param.Add("Pakai", StanKini - StanLalu);
            if (!string.IsNullOrWhiteSpace(NewStanLalu.Text) && !string.IsNullOrWhiteSpace(NewStanKini.Text) && StanLalu <= StanKini)
            {
                if (ViewModel != null)
                {
                    StanLalu2.Text = StanLalu.ToString();
                    StanKini2.Text = StanKini.ToString();
                    _ = ((AsyncCommandBase)ViewModel.OnHitungCommand).ExecuteAsync(param);

                }
            }
            else
            {
                StanLalu2.Text = "-";
                StanKini2.Text = "-";
                StanAngkat2.Text = "-";
                Pakai2.Text = "-";
                BiayaPemakaian2.Text = "-";
                Administrasi2.Text = "-";
                AdministrasiLain2.Text = "-";
                Pemeliharaan2.Text = "-";
                PemeliharaanLain2.Text = "-";
                Retribusi2.Text = "-";
                RetribusiLain2.Text = "-";
                Materai2.Text = "-";
                Tagihan2.Text = "-";
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var Param = new ParamRekeningAirPengembalianDto();
            if (!ViewModel.IsAdd)
            {
                Param.IdPengembalian = ViewModel.SelectedData.IdPengembalian;
            }
            Param.TanggalBa = TanggalBA.SelectedDate;
            Param.Alasan = Alasan.Text;
            Param.IdPelangganAir = ViewModel.SelectedPelanggan.IdPelangganAir;
            Param.IdPeriode = ViewModel.SelectedPeriodeTransaksi.IdPeriode;
            Param.IdRayon = ViewModel.SelectedPelanggan.IdRayon;
            Param.IdKelurahan = ViewModel.SelectedPelanggan.IdKelurahan;
            Param.IdGolongan = ViewModel.SelectedPelanggan.IdGolongan;
            Param.IdDiameter = ViewModel.SelectedPelanggan.IdDiameter;
            Param.StanLalu_Lama = ViewModel.SelectedPeriodeTransaksi.StanLalu;
            Param.StanSkrg_Lama = ViewModel.SelectedPeriodeTransaksi.StanSkrg;
            Param.StanAngkat_Lama = ViewModel.SelectedPeriodeTransaksi.StanAngkat;
            Param.Pakai_Lama = ViewModel.SelectedPeriodeTransaksi.Pakai;
            Param.BiayaPemakaian_Lama = ViewModel.SelectedPeriodeTransaksi.BiayaPemakaian;
            Param.AdministrasiLain_Lama = ViewModel.SelectedPeriodeTransaksi.Administrasi;
            Param.PemeliharaanLain_Lama = ViewModel.SelectedPeriodeTransaksi.Pemeliharaan;
            Param.RetribusiLain_Lama = ViewModel.SelectedPeriodeTransaksi.Retribusi;
            Param.Pelayanan_Lama = ViewModel.SelectedPeriodeTransaksi.Pelayanan;
            Param.AirLimbah_Lama = ViewModel.SelectedPeriodeTransaksi.AirLimbah;
            Param.DendaPakai0_Lama = ViewModel.SelectedPeriodeTransaksi.DendaPakai0;
            Param.AdministrasiLain_Lama = ViewModel.SelectedPeriodeTransaksi.AdministrasiLain;
            Param.PemeliharaanLain_Lama = ViewModel.SelectedPeriodeTransaksi.PemeliharaanLain;
            Param.RetribusiLain_Lama = ViewModel.SelectedPeriodeTransaksi.RetribusiLain;
            Param.Meterai_Lama = ViewModel.SelectedPeriodeTransaksi.Rekair;
            Param.Denda_Lama = ViewModel.SelectedPeriodeTransaksi.Denda;
            Param.Ppn_Lama = 0;
            Param.Total_Lama = ViewModel.SelectedPeriodeTransaksi.Total;
            Param.StanLalu = DecimalValidationHelper.FormatStringIdToDecimal(NewStanLalu.Text);
            Param.StanSkrg = DecimalValidationHelper.FormatStringIdToDecimal(NewStanKini.Text);
            Param.StanAngkat = 0;
            Param.Pakai = ViewModel.KalkulasiRekening.Pakai;
            Param.BiayaPemakaian = ViewModel.KalkulasiRekening.BiayaPemakaian;
            Param.Administrasi = ViewModel.KalkulasiRekening.Administrasi;
            Param.Pemeliharaan = ViewModel.KalkulasiRekening.Pemeliharaan;
            Param.Retribusi = ViewModel.KalkulasiRekening.Retribusi;
            Param.Pelayanan = ViewModel.KalkulasiRekening.Pelayanan;
            Param.AirLimbah = ViewModel.KalkulasiRekening.AirLimbah;
            Param.DendaPakai0 = ViewModel.KalkulasiRekening.DendaPakai0;
            Param.AdministrasiLain = ViewModel.KalkulasiRekening.AdministrasiLain;
            Param.PemeliharaanLain = ViewModel.KalkulasiRekening.PemeliharaanLain;
            Param.RetribusiLain = ViewModel.KalkulasiRekening.RetribusiLain;
            Param.Meterai = ViewModel.KalkulasiRekening.Meterai;
            Param.Rekair = ViewModel.KalkulasiRekening.Rekair;
            Param.Denda = 0;
            Param.Ppn = ViewModel.KalkulasiRekening.Ppn;
            Param.Total = ViewModel.KalkulasiRekening.Rekair;
            Param.Blok1 = ViewModel.KalkulasiRekening.Blok1;
            Param.Blok2 = ViewModel.KalkulasiRekening.Blok2;
            Param.Blok3 = ViewModel.KalkulasiRekening.Blok3;
            Param.Blok4 = ViewModel.KalkulasiRekening.Blok4;
            Param.Blok5 = ViewModel.KalkulasiRekening.Blok5;
            Param.Prog1 = ViewModel.KalkulasiRekening.Prog1;
            Param.Prog2 = ViewModel.KalkulasiRekening.Prog2;
            Param.Prog3 = ViewModel.KalkulasiRekening.Prog3;
            Param.Prog4 = ViewModel.KalkulasiRekening.Prog4;
            Param.Prog5 = ViewModel.KalkulasiRekening.Prog5;
            Param.Keterangan = Alasan.Text;
            Param.IdKondisiMeter = ViewModel.SelectedKondisiMeter.IdKondisiMeter;
            Param.IdUser = ViewModel.SelectedMasterPegawai.IdPegawai;

            _ = Task.Run(() => ((AsyncCommandBase)ViewModel.OnShowConfirmSubmitCommand).ExecuteAsync(Param));
        }

        private void Petugas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KondisiMeter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }


    }
}
