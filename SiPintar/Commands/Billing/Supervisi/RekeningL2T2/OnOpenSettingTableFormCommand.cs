using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningL2T2;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTest)
                return;

            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_rekening_lltt_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Publish = data["show_table_column"]["publish"] == "1",
                NoLltt = data["show_table_column"]["no_lltt"] == "1",
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                Biaya = data["show_table_column"]["biaya"] == "1",
                Upload = data["show_table_column"]["upload"] == "1",
                KodeLltt = data["show_table_column"]["kode_lltt"] == "1",
                TarifLltt = data["show_table_column"]["tarif_lltt"] == "1",
                KodeRayon = data["show_table_column"]["kode_rayon"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Lunas = data["show_table_column"]["lunas"] == "1",
                TglBayar = data["show_table_column"]["tgl_bayar"] == "1",
                KodeWilayah = data["show_table_column"]["kode_wilayah"] == "1",
                Wilayah = data["show_table_column"]["wilayah"] == "1",
                WaktuPublish = data["show_table_column"]["waktu_publish"] == "1",
                Flag = data["show_table_column"]["flag"] == "1",
                Status = data["show_table_column"]["status"] == "1"
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableFormView(_viewModel), "BillingRootDialog");
        }
    }
}
