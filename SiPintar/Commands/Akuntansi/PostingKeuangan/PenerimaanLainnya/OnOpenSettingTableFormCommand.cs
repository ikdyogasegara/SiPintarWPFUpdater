using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PenerimaanLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\posting_keuangan_penerimaanlainnya_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NoTrans = data["show_table_column"]["notrans"] == "1",
                KodeWilayah = data["show_table_column"]["kode_wilayah"] == "1",
                NamaWilayah = data["show_table_column"]["nama_wilayah"] == "1",
                NamaDebet = data["show_table_column"]["nama_debet"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                WaktuInput = data["show_table_column"]["waktu_input"] == "1",
                JumlahNominal = data["show_table_column"]["jumlah_nominal"] == "1",
                KodeKredit = data["show_table_column"]["kode_kredit"] == "1",
                NamaKredit = data["show_table_column"]["nama_kredit"] == "1"
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
