using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTest)
                return;

            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Bacameter\\supervisi_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                IdPelanggan = data["table_supervisi"]["id_pelanggan"] == "1",
                NamaPelanggan = data["table_supervisi"]["nama_pelanggan"] == "1",
                Status = data["table_supervisi"]["status_aktif"] == "1",
                Verifikasi = data["table_supervisi"]["status_verifikasi"] == "1",
                StanAwal = data["table_supervisi"]["stan_awal"] == "1",
                StanAkhir = data["table_supervisi"]["stan_akhir"] == "1",
                Pakai = data["table_supervisi"]["pakai"] == "1",
                StanAngkat = data["table_supervisi"]["stan_angkat"] == "1",
                BiayaPemakaian = data["table_supervisi"]["biayapemakaian"] == "1",
                Administrasi = data["table_supervisi"]["administrasi"] == "1",
                Pemeliharaan = data["table_supervisi"]["pemeliharaan"] == "1",
                Retribusi = data["table_supervisi"]["retribusi"] == "1",
                Pelayanan = data["table_supervisi"]["pelayanan"] == "1",
                AirLimbah = data["table_supervisi"]["airlimbah"] == "1",
                DendaPakai0 = data["table_supervisi"]["dendapakai0"] == "1",
                AdministrasiLain = data["table_supervisi"]["administrasilain"] == "1",
                PemeliharaanLain = data["table_supervisi"]["pemeliharaanlain"] == "1",
                RetribusiLain = data["table_supervisi"]["retribusilain"] == "1",
                Ppn = data["table_supervisi"]["ppn"] == "1",
                Meterai = data["table_supervisi"]["meterai"] == "1",
                Rekair = data["table_supervisi"]["rekair"] == "1",
                Denda = data["table_supervisi"]["denda"] == "1",
                Total = data["table_supervisi"]["total"] == "1",
                MerekMeter = data["table_supervisi"]["merek_meter"] == "1",
                KodeRayon = data["table_supervisi"]["kode_rayon"] == "1",
                Rayon = data["table_supervisi"]["rayon"] == "1",
                Alamat = data["table_supervisi"]["alamat"] == "1",
                NoSambungan = data["table_supervisi"]["no_sambungan"] == "1",
                KodeGolongan = data["table_supervisi"]["kode_golongan"] == "1",
                Golongan = data["table_supervisi"]["golongan"] == "1",
                Diameter = data["table_supervisi"]["diameter"] == "1",
                Kelainan = data["table_supervisi"]["kelainan"] == "1",
                SumberModul = data["table_supervisi"]["sumber_modul"] == "1",
                NamaPetugas = data["table_supervisi"]["nama_petugas"] == "1",
                WaktuVerifikasi = data["table_supervisi"]["waktu_verifikasi"] == "1",
                WaktuBaca = data["table_supervisi"]["waktu_baca"] == "1",
                Lampiran = data["table_supervisi"]["lampiran"] == "1",
                WideRowHeight = data["table_supervisi"]["wide_row_height"] == "1",
                NormalRowHeight = data["table_supervisi"]["normal_row_height"] == "1",
                NarrowRowHeight = data["table_supervisi"]["narrow_row_height"] == "1"
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableFormView(_viewModel), "BacameterRootDialog");
        }
    }
}
