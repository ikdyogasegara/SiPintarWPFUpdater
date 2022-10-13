using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_rekening_air_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                FlagPublish = data["show_table_column"]["flag_publish"] == "1",
                Verifikasi = data["show_table_column"]["flag_verifikasi"] == "1",
                Nosamb = data["show_table_column"]["nosamb"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                NamaGolongan = data["show_table_column"]["nama_golongan"] == "1",
                NamaDiameter = data["show_table_column"]["nama_diameter"] == "1",
                KodeCabang = data["show_table_column"]["kode_cabang"] == "1",
                NamaCabang = data["show_table_column"]["nama_cabang"] == "1",
                KodeRayon = data["show_table_column"]["kode_rayon"] == "1",
                NamaRayon = data["show_table_column"]["nama_rayon"] == "1",
                KodeKelurahan = data["show_table_column"]["kode_kelurahan"] == "1",
                NamaKelurahan = data["show_table_column"]["nama_kelurahan"] == "1",
                Flag = data["show_table_column"]["id_flag"] == "1",
                NamaFlag = data["show_table_column"]["nama_flag"] == "1",
                Kelainan = data["show_table_column"]["kelainan"] == "1",
                Taksasi = data["show_table_column"]["taksasi"] == "1",
                PetugasBaca = data["show_table_column"]["petugas_baca"] == "1",
                KodeKolektif = data["show_table_column"]["kode_kolektif"] == "1",
                NamaKolektif = data["show_table_column"]["nama_kolektif"] == "1",
                NoRekening = data["show_table_column"]["no_rekening"] == "1",
                NamaStatus = data["show_table_column"]["nama_status"] == "1",

                StanLalu = data["show_table_column"]["stan_lalu"] == "1",
                StanSkrg = data["show_table_column"]["stan_skrg"] == "1",
                StanAngkat = data["show_table_column"]["stan_angkat"] == "1",
                Pakai = data["show_table_column"]["pakai"] == "1",
                PakaiHitung = data["show_table_column"]["pakai_kalkulasi"] == "1",
                BiayaPemakaian = data["show_table_column"]["biaya_pemakaian"] == "1",
                Administrasi = data["show_table_column"]["administrasi"] == "1",
                Pemeliharaan = data["show_table_column"]["pemelaiharaan"] == "1",
                Retribusi = data["show_table_column"]["retribusi"] == "1",
                Meterai = data["show_table_column"]["meterai"] == "1",
                Rekair = data["show_table_column"]["rekair"] == "1",
                PemeliharaanLain = data["show_table_column"]["pemeliharaan_lain"] == "1",
                AdministrasiLain = data["show_table_column"]["administrasi_lain"] == "1",
                RetribusiLain = data["show_table_column"]["retribusi_lain"] == "1",
                DendaPakai0 = data["show_table_column"]["denda_pakai0"] == "1",
                Denda = data["show_table_column"]["denda"] == "1",
                Total = data["show_table_column"]["total"] == "1",
                AirLimbah = data["show_table_column"]["air_limbah"] == "1",
                Pelayanan = data["show_table_column"]["pelayanan"] == "1",
                Ppn = data["show_table_column"]["ppn"] == "1",

                FlagKoreksi = data["show_table_column"]["flag_koreksi"] == "1",
                KodeGolongan = data["show_table_column"]["kode_golongan"] == "1",
                KodeDiameter = data["show_table_column"]["kode_diameter"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                NamaUser = data["show_table_column"]["nama_user"] == "1",
                NamaLoket = data["show_table_column"]["nama_loket"] == "1",
                WaktuTransaksi = data["show_table_column"]["waktu_transaksi"] == "1",
                WaktuKoreksi = data["show_table_column"]["waktu_koreksi"] == "1",
                WaktuPublish = data["show_table_column"]["waktu_publish"] == "1",
                FlagUpload = data["show_table_column"]["flag_upload"] == "1",
                KodeWilayah = data["show_table_column"]["kode_wilayah"] == "1",
                NamaWilayah = data["show_table_column"]["nama_wilayah"] == "1",
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableView(_viewModel), "BillingRootDialog");
        }
    }
}
