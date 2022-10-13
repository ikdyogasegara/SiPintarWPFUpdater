using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Voucher;
using SiPintar.Views.Akuntansi.Voucher.IsiEdit;

namespace SiPintar.Commands.Akuntansi.Voucher.IsiEdit
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly IsiEditViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(IsiEditViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\voucher_isiedit_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NomorBuku = data["show_table_column"]["nomor_buku"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                BebanBagian = data["show_table_column"]["beban_bagian"] == "1",
                DibayarkanKepada = data["show_table_column"]["dibayarkan_kepada"] == "1",
                Jumlah = data["show_table_column"]["jumlah"] == "1",
                TglTransaksi = data["show_table_column"]["tgl_transaksi"] == "1"
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
