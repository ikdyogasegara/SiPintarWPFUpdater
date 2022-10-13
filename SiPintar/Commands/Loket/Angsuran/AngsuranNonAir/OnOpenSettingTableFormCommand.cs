using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.SettingTable;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranNonAir
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly AngsuranNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(AngsuranNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Loket\\angsuran_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Nama = data["non_air_lainnya"]["nama"] == "1",
                JenisAngsuran = data["non_air_lainnya"]["jenis_angsuran"] == "1",
                NoAngsuran = data["non_air_lainnya"]["no_angsuran"] == "1",
                Termin = data["non_air_lainnya"]["termin"] == "1",
                Jumlah = data["non_air_lainnya"]["jumlah"] == "1",
                Alamat = data["non_air_lainnya"]["alamat"] == "1",
                WaktuDaftar = data["non_air_lainnya"]["waktu_daftar"] == "1",
                DibebankanKepada = data["non_air_lainnya"]["dibebankan_kepada"] == "1",
                NoSamb = data["non_air_lainnya"]["no_samb"] == "1",
                NomorBA = data["non_air_lainnya"]["no_berita_acara"] == "1",
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableNonAirLainnyaView(_viewModel), "LoketRootDialog");
        }
    }
}
