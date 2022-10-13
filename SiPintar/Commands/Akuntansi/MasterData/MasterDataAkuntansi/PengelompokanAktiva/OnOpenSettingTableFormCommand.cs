using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PengelompokanAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PengelompokanAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_pengelompokan_aktiva_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                GolAktiva = data["show_table_column"]["GolAktiva"] == "1",
                UraianAktiva = data["show_table_column"]["UraianAktiva"] == "1",
                JumlahTahun = data["show_table_column"]["JumlahTahun"] == "1",
                TipeAktiva = data["show_table_column"]["TipeAktiva"] == "1",
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
