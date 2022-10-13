using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(UsulanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_usulan_koreksi_rekening_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                NomorRegister = data["show_table_column"]["NomorRegister"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                Rayon = data["show_table_column"]["Rayon"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                NomorBeritaAcara = data["show_table_column"]["NomorBeritaAcara"] == "1",
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new TableSettingFormView(_viewModel), "HublangRootDialog");
        }
    }
}
