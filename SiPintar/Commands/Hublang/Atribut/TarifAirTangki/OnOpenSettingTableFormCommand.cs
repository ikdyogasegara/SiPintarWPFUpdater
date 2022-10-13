using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Hublang.Atribut.TarifAirTangki;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\atribut_tarif_tangki_air_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodeTarif = data["show_table_column"]["KodeTarif"] == "1",
                Kategori = data["show_table_column"]["Kategori"] == "1",
                NamaTarif = data["show_table_column"]["NamaTarif"] == "1",
                BiayaAir = data["show_table_column"]["BiayaAir"] == "1",
                BiayaAdministrasi = data["show_table_column"]["BiayaAdministrasi"] == "1",
                BiayaOperasional = data["show_table_column"]["BiayaOperasional"] == "1",
                BiayaTransport = data["show_table_column"]["BiayaTransport"] == "1",
                Ppn = data["show_table_column"]["Ppn"] == "1"
            };

            ShowDialog();
            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "HublangRootDialog"); }
        }
    }
}
