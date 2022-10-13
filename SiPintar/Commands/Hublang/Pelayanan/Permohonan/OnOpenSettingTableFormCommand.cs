using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PermohonanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_permohonan_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Jenis = data["show_table_column"]["Jenis"] == "1",
                Status = data["show_table_column"]["Status"] == "1",
                NomorRegister = data["show_table_column"]["NomorRegister"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                Biaya = data["show_table_column"]["Biaya"] == "1",
                UserInput = data["show_table_column"]["UserInput"] == "1",
                UserBeritaAcara = data["show_table_column"]["UserBeritaAcara"] == "1"
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
