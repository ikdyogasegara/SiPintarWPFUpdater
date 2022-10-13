using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.SettingTable;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(AngsuranSambunganBaruViewModel viewModel, bool isTest = false)
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
                Nama = data["sambungan_baru"]["nama"] == "1",
                JenisAngsuran = data["sambungan_baru"]["jenis_angsuran"] == "1",
                NoAngsuran = data["sambungan_baru"]["no_angsuran"] == "1",
                Termin = data["sambungan_baru"]["termin"] == "1",
                Jumlah = data["sambungan_baru"]["jumlah"] == "1",
                Alamat = data["sambungan_baru"]["alamat"] == "1",
                WaktuDaftar = data["sambungan_baru"]["waktu_daftar"] == "1",
                DibebankanKepada = data["sambungan_baru"]["dibebankan_kepada"] == "1",
                NoSamb = data["sambungan_baru"]["no_samb"] == "1",
                NomorBA = data["sambungan_baru"]["no_berita_acara"] == "1",
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableSambunganBaruView(_viewModel), "LoketRootDialog");
        }
    }
}
