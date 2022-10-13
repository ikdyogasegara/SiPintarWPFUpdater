using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.TagihanManual;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_tagihan_manual_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                JenisTipePelanggan = data["show_table_column"]["JenisTipePelanggan"] == "1",
                NomorNonAir = data["show_table_column"]["NomorNonAir"] == "1",
                KodeJenisNonAir = data["show_table_column"]["KodeJenisNonAir"] == "1",
                NamaJenisNonAir = data["show_table_column"]["NamaJenisNonAir"] == "1",
                Total = data["show_table_column"]["Total"] == "1",
                NomorPelanggan = data["show_table_column"]["NomorPelanggan"] == "1",
                Nama = data["show_table_column"]["Nama"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Keterangan = data["show_table_column"]["Keterangan"] == "1",
                KodeTarif = data["show_table_column"]["KodeTarif"] == "1",
                NamaTarif = data["show_table_column"]["NamaTarif"] == "1",
                KodeRayon = data["show_table_column"]["KodeRayon"] == "1",
                NamaRayon = data["show_table_column"]["NamaRayon"] == "1",
                KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                KodeKelurahan = data["show_table_column"]["KodeKelurahan"] == "1",
                NamaKelurahan = data["show_table_column"]["NamaKelurahan"] == "1"
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
