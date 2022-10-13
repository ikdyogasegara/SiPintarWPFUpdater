using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.Periode;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTest)
                return;

            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_periode_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Bulan = data["show_table_column"]["bulan"] == "1",
                TglMulaiTagih = data["show_table_column"]["tgl_mulai_tagih"] == "1",
                PelangganAir = data["show_table_column"]["pelanggan_air"] == "1",
                PelangganLimbah = data["show_table_column"]["pelanggan_limbah"] == "1",
                PelangganL2T2 = data["show_table_column"]["pelanggan_l2t2"] == "1",
                RekeningAir = data["show_table_column"]["rekening_air"] == "1",
                RekeningLimbah = data["show_table_column"]["rekening_limbah"] == "1",
                RekeningL2T2 = data["show_table_column"]["rekening_l2t2"] == "1",
                Status = data["show_table_column"]["status"] == "1",
                JumlahPakaiAir = data["show_table_column"]["jumlahpakaiair"] == "1",
                JumlahKelainan = data["show_table_column"]["jumlahkelainan"] == "1",
                JumlahTaksir = data["show_table_column"]["jumlahtaksir"] == "1",
                PelangganAirPublish = data["show_table_column"]["pelangganairpublish"] == "1",
                PelangganLimbahPublish = data["show_table_column"]["pelangganlimbahpublish"] == "1",
                PelangganLlttPublish = data["show_table_column"]["pelangganllttpublish"] == "1"
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
