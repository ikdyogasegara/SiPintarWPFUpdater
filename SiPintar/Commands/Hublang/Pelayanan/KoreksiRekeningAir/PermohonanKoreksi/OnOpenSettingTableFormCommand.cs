using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PermohonanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoading = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_permohonan_koreksi_rekening_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Jenis = data["show_table_column"]["Jenis"] == "1",
                Status = data["show_table_column"]["Status"] == "1",
                NomorRegister = data["show_table_column"]["NomorRegister"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                Rayon = data["show_table_column"]["Rayon"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1"
            };

            ShowDialog();
            _viewModel.Parent.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "HublangRootDialog"); }
        }
    }
}
