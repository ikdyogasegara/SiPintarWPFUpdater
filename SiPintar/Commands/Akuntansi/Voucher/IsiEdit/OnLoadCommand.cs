using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Voucher;
using SiPintar.Views;

namespace SiPintar.Commands.Akuntansi.Voucher.IsiEdit
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly IsiEditViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(IsiEditViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _ = _viewModel;
            _ = _restApi;

            TableColumnConfiguration();

            string errorMessage = null;
            _viewModel.IsLoading = true;

            ShowResult(errorMessage);
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private void ShowResult(string errorMessage)
        {
            if (errorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate { if (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) is AkuntansiView mainView) mainView.ShowSnackbar(errorMessage, "danger"); });
        }

        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\voucher_isiedit_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NomorBuku = data["show_table_column"]["nomor_buku"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                BebanBagian = data["show_table_column"]["beban_bagian"] == "1",
                DibayarkanKepada = data["show_table_column"]["dibayarkan_kepada"] == "1",
                Jumlah = data["show_table_column"]["jumlah"] == "1",
                TglTransaksi = data["show_table_column"]["tgl_transaksi"] == "1"
            };
        }
    }
}
