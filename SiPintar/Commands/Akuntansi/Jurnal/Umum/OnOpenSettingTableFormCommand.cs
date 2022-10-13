using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views.Akuntansi.Jurnal.Umum;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(UmumViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\jurnal_umum_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NomorBukti = data["show_table_column"]["nomor_bukti"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                Jumlah = data["show_table_column"]["jumlah"] == "1",
                TanggalTransaksi = data["show_table_column"]["tanggal_transaksi"] == "1"
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog");
        }
    }
}
