using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Pendaftaran;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnOpenTableSettingCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenTableSettingCommand(PendaftaranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pendaftaran_sambungan_baru_config.ini";

            if (!File.Exists(configIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Flag = data["show_table_column"]["flag"] == "1",
                NomorPendaftaran = data["show_table_column"]["nomor_pendaftaran"] == "1",
                Tanggal = data["show_table_column"]["tanggal"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                NoHandphone = data["show_table_column"]["no_handphone"] == "1",
                NoSambDepan = data["show_table_column"]["no_samb_depan"] == "1",
                NoSambBelakang = data["show_table_column"]["no_samb_belakang"] == "1",
                NoSambKiri = data["show_table_column"]["no_samb_kiri"] == "1",
                NoSambKanan = data["show_table_column"]["no_samb_kanan"] == "1",
                Cabang = data["show_table_column"]["cabang"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Penghuni = data["show_table_column"]["penghuni"] == "1",
                JenisBangunan = data["show_table_column"]["jenis_bangunan"] == "1",
                Pekerjaan = data["show_table_column"]["pekerjaan"] == "1",
                Biaya = data["show_table_column"]["biaya"] == "1",
                Tipe = data["show_table_column"]["tipe"] == "1",
                User = data["show_table_column"]["user"] == "1",
                NomorRAB = data["show_table_column"]["nomor_RAB"] == "1",
                TanggalRAB = data["show_table_column"]["tanggal_RAB"] == "1",
                NomorSPPRAB = data["show_table_column"]["nomor_SPPRAB"] == "1",
                TanggalSPPRAB = data["show_table_column"]["tanggal_SPPRAB"] == "1",
                NomorBA = data["show_table_column"]["nomor_BA"] == "1",
                NomorSPKO = data["show_table_column"]["nomor_SPKO"] == "1",
                NomorSPKP = data["show_table_column"]["nomor_SPKP"] == "1",
                NomorSPA = data["show_table_column"]["nomor_SPA"] == "1"
            };

            ShowDialog();

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
