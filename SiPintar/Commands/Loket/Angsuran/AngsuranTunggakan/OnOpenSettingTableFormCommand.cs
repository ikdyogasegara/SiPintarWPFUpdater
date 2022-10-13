using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.SettingTable;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
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
                Nama = data["tunggakan"]["nama"] == "1",
                JenisAngsuran = data["tunggakan"]["jenis_angsuran"] == "1",
                NoAngsuran = data["tunggakan"]["no_angsuran"] == "1",
                Termin = data["tunggakan"]["termin"] == "1",
                Jumlah = data["tunggakan"]["jumlah"] == "1",
                Alamat = data["tunggakan"]["alamat"] == "1",
                WaktuDaftar = data["tunggakan"]["waktu_daftar"] == "1",
                DibebankanKepada = data["tunggakan"]["dibebankan_kepada"] == "1",
                NoSamb = data["tunggakan"]["no_samb"] == "1",
                NomorBA = data["tunggakan"]["no_berita_acara"] == "1",
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableTunggakanView(_viewModel), "LoketRootDialog");
        }
    }
}
