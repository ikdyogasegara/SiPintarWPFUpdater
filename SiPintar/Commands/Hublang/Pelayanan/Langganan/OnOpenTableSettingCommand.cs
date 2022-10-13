using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Langganan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnOpenTableSettingCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenTableSettingCommand(LanggananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            if (!_isTest)
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pelanggan_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                _viewModel.TableSetting = new
                {
                    Nama = data["show_table_column"]["nama"] == "1",
                    Alamat = data["show_table_column"]["alamat"] == "1",
                    Rayon = data["show_table_column"]["rayon"] == "1",
                    Area = data["show_table_column"]["area"] == "1",
                    Wilayah = data["show_table_column"]["wilayah"] == "1",
                    Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                    Kecamatan = data["show_table_column"]["kecamatan"] == "1",
                    Cabang = data["show_table_column"]["cabang"] == "1",
                    Kolektif = data["show_table_column"]["kolektif"] == "1",
                    Flag = data["show_table_column"]["flag"] == "1"
                };

                ShowDialog();
            }

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new TableSettingFormView(_viewModel), "HublangRootDialog");
        }
    }
}
