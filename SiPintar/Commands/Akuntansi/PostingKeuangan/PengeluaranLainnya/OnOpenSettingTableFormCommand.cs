using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PengeluaranLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    [ExcludeFromCodeCoverage]

    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PengeluaranLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\posting_keuangan_pengeluaranlainnya_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NomorTransaksi = data["show_table_column"]["NomorTransaksi"] == "1",
                KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                KodePerkiraanDebet = data["show_table_column"]["KodePerkiraanDebet"] == "1",
                NamaPerkiraanDebet = data["show_table_column"]["NamaPerkiraanDebet"] == "1",
                KodePerkiraanKredit = data["show_table_column"]["KodePerkiraanKredit"] == "1",
                NamaPerkiraanKredit = data["show_table_column"]["NamaPerkiraanKredit"] == "1",
                Uraian = data["show_table_column"]["Uraian"] == "1",
                JumlahNominal = data["show_table_column"]["JumlahNominal"] == "1",
                TanggalTerima = data["show_table_column"]["TanggalTerima"] == "1"
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
