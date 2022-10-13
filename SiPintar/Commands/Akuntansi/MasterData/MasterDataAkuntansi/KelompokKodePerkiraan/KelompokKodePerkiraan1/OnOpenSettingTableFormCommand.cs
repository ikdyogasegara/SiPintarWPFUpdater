using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(KelompokKodePerkiraan1ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoadingForm = true;
            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_kelompok_kode_perkiraan_1_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodePerkiraan = data["show_table_column"]["KodePerkiraan"] == "1",
                NamaPerkiraan = data["show_table_column"]["NamaPerkiraan"] == "1",
            };

            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog"); }
            _viewModel.Parent.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }
    }
}
