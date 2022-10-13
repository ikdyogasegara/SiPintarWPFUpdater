using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganL2T2;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PelangganL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTest)
                return;

            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_pelanggan_lltt_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NoLltt = data["show_table_column"]["no_lltt"] == "1",
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                KodeLltt = data["show_table_column"]["kode_lltt"] == "1",
                GolLltt = data["show_table_column"]["gol_lltt"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Kolektif = data["show_table_column"]["kolektif"] == "1",
                NoHp = data["show_table_column"]["no_hp"] == "1",
                NoTelp = data["show_table_column"]["no_telp"] == "1",
                Ktp = data["show_table_column"]["ktp"] == "1",
                Email = data["show_table_column"]["email"] == "1",
                Keterangan = data["show_table_column"]["keterangan"] == "1",
                Status = data["show_table_column"]["status"] == "1",
                Flag = data["show_table_column"]["flag"] == "1"
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
