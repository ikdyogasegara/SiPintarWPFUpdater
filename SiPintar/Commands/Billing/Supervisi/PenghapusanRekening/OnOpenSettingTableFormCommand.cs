using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PenghapusanRekening;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTest)
                return;

            _viewModel.IsLoadingForm = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_penghapusan_rekening_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Golongan = data["show_table_column"]["golongan"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Wilayah = data["show_table_column"]["wilayah"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",

                StanLalu = data["show_table_column"]["stan_lalu"] == "1",
                StanSkrg = data["show_table_column"]["stan_skrg"] == "1",
                StanAngkat = data["show_table_column"]["stan_angkat"] == "1",
                Pakai = data["show_table_column"]["pakai"] == "1",
                BiayaPemakaian = data["show_table_column"]["biaya_pemakaian"] == "1",
                Administrasi = data["show_table_column"]["administrasi"] == "1",
                Pemeliharaan = data["show_table_column"]["pemelaiharaan"] == "1",
                Retribusi = data["show_table_column"]["retribusi"] == "1",
                Meterai = data["show_table_column"]["meterai"] == "1",
                PemeliharaanLain = data["show_table_column"]["pemeliharaan_lain"] == "1",
                AdministrasiLain = data["show_table_column"]["administrasi_lain"] == "1",
                RetribusiLain = data["show_table_column"]["retribusi_lain"] == "1",
                DendaPakai0 = data["show_table_column"]["denda_pakai0"] == "1",
                AirLimbah = data["show_table_column"]["air_limbah"] == "1",
                Pelayanan = data["show_table_column"]["pelayanan"] == "1",
                Ppn = data["show_table_column"]["ppn"] == "1",
                Rekair = data["show_table_column"]["rekair"] == "1",

            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableFormView(_viewModel), "BillingRootDialog");
        }
    }
}
