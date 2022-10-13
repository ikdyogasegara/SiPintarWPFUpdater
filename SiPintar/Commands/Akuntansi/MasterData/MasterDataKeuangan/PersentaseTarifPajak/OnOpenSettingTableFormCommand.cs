using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PersentaseTarifPajakViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_keuangan_persentase_tarif_pajak_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodePersen = data["show_table_column"]["KodePersen"] == "1",
                NamaPersen = data["show_table_column"]["NamaPersen"] == "1",
                JumlahPersen = data["show_table_column"]["JumlahPersen"] == "1",
            };

            ShowDialog();
            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog"); }
        }
    }
}
