using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_saldo_awal_perkiraan_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodePerkiraan = data["show_table_column"]["KodePerkiraan"] == "1",
                NamaPerkiraan = data["show_table_column"]["NamaPerkiraan"] == "1",
                SaldoAwal = data["show_table_column"]["SaldoAwal"] == "1",
                SaldoAkhir = data["show_table_column"]["SaldoAkhir"] == "1",
                TanggalSaldo = data["show_table_column"]["TanggalSaldo"] == "1",
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
