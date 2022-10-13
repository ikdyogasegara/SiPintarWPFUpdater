using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(InteraksiPenyusutanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoading = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_penyusutan_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                AkunAktiva = data["show_table_column"]["AkunAktiva"] == "1",
                NamaAktiva = data["show_table_column"]["NamaAktiva"] == "1",
                AkunPenyusutan = data["show_table_column"]["AkunPenyusutan"] == "1",
                AkumulasiPenyusutan = data["show_table_column"]["AkumulasiPenyusutan"] == "1",
            };

            ShowDialog();
            _viewModel.Parent.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog"); }
        }
    }
}
