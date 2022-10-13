using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(UmumViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            TableColumnConfiguration();

            string errorMessage = null;
            _viewModel.IsEmpty = false;
            _viewModel.IsLoading = true;

            ShowResult(errorMessage);
            _viewModel.IsLoading = false;
            _viewModel.IsEmpty = true;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string errorMessage)
        {
            if (errorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate { if (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) is AkuntansiView mainView) mainView.ShowSnackbar(errorMessage, "danger"); });
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\jurnal_umum_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NomorBukti = data["show_table_column"]["nomor_bukti"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                Jumlah = data["show_table_column"]["jumlah"] == "1",
                TanggalTransaksi = data["show_table_column"]["tanggal_transaksi"] == "1"
            };
        }
    }
}
