using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.BAPengembalian;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(BaPengembalianViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_berita_acara_pengembalian_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1",
                NomorSambungan = data["show_table_column"]["NomorSambungan"] == "1",
                Bulan = data["show_table_column"]["Bulan"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                RekairLama = data["show_table_column"]["RekairLama"] == "1",
                RekairBaru = data["show_table_column"]["RekairBaru"] == "1",
                Gol = data["show_table_column"]["Gol"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                Rayon = data["show_table_column"]["Rayon"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                KondisiMeter = data["show_table_column"]["KondisiMeter"] == "1",
                Keterangan = data["show_table_column"]["Keterangan"] == "1",
                TanggalInput = data["show_table_column"]["TanggalInput"] == "1",
                UserInput = data["show_table_column"]["UserInput"] == "1",
            };

            ShowDialog();
            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "HublangRootDialog"); }
        }

    }
}
