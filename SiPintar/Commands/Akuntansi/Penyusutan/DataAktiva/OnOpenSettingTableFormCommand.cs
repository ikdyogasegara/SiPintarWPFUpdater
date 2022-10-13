using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Penyusutan;
using SiPintar.Views.Akuntansi.Penyusutan.DataAktiva;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(DataAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\penyusutan_data_aktiva_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Uraian = data["show_table_column"]["uraian"] == "1",
                Tahun = data["show_table_column"]["tahun"] == "1",
                Lokasi = data["show_table_column"]["lokasi"] == "1",
                Asal = data["show_table_column"]["asal"] == "1",
                Tanggal = data["show_table_column"]["tanggal"] == "1",
                Nilai = data["show_table_column"]["nilai"] == "1",
                Mutasi = data["show_table_column"]["mutasi"] == "1",
                Perolehan = data["show_table_column"]["perolehan"] == "1",
                JmlTahun = data["show_table_column"]["jml_tahun"] == "1",
                Persen = data["show_table_column"]["persen"] == "1",
                NilaiBukuLalu = data["show_table_column"]["nilai_buku_lalu"] == "1",
                AkunPenyLalu = data["show_table_column"]["akun_peny_lalu"] == "1",
                PenyusutanJanuari = data["show_table_column"]["penyusutan_januari"] == "1",
                PenyusutanFebruari = data["show_table_column"]["penyusutan_februari"] == "1",
                PenyusutanMaret = data["show_table_column"]["penyusutan_maret"] == "1",
                PenyusutanApril = data["show_table_column"]["penyusutan_april"] == "1",
                PenyusutanMei = data["show_table_column"]["penyusutan_mei"] == "1",
                PenyusutanJuni = data["show_table_column"]["penyusutan_juni"] == "1",
                PenyusutanJuli = data["show_table_column"]["penyusutan_juli"] == "1",
                PenyusutanAgustus = data["show_table_column"]["penyusutan_agustus"] == "1",
                PenyusutanSeptember = data["show_table_column"]["penyusutan_september"] == "1",
                PenyusutanOktober = data["show_table_column"]["penyusutan_oktober"] == "1",
                PenyusutanNovember = data["show_table_column"]["penyusutan_november"] == "1",
                PenyusutanDesember = data["show_table_column"]["penyusutan_desember"] == "1",
                AkunPenyusutanJanuari = data["show_table_column"]["akun_penyusutan_januari"] == "1",
                AkunPenyusutanFebruari = data["show_table_column"]["akun_penyusutan_februari"] == "1",
                AkunPenyusutanMaret = data["show_table_column"]["akun_penyusutan_maret"] == "1",
                AkunPenyusutanApril = data["show_table_column"]["akun_penyusutan_april"] == "1",
                AkunPenyusutanMei = data["show_table_column"]["akun_penyusutan_mei"] == "1",
                AkunPenyusutanJuni = data["show_table_column"]["akun_penyusutan_juni"] == "1",
                AkunPenyusutanJuli = data["show_table_column"]["akun_penyusutan_juli"] == "1",
                AkunPenyusutanAgustus = data["show_table_column"]["akun_penyusutan_agustus"] == "1",
                AkunPenyusutanSeptember = data["show_table_column"]["akun_penyusutan_september"] == "1",
                AkunPenyusutanOktober = data["show_table_column"]["akun_penyusutan_oktober"] == "1",
                AkunPenyusutanNovember = data["show_table_column"]["akun_penyusutan_november"] == "1",
                AkunPenyusutanDesember = data["show_table_column"]["akun_penyusutan_desember"] == "1",
                NilaiBukuJanuari = data["show_table_column"]["nilai_buku_januari"] == "1",
                NilaiBukuFebruari = data["show_table_column"]["nilai_buku_februari"] == "1",
                NilaiBukuMaret = data["show_table_column"]["nilai_buku_maret"] == "1",
                NilaiBukuApril = data["show_table_column"]["nilai_buku_april"] == "1",
                NilaiBukuMei = data["show_table_column"]["nilai_buku_mei"] == "1",
                NilaiBukuJuni = data["show_table_column"]["nilai_buku_juni"] == "1",
                NilaiBukuJuli = data["show_table_column"]["nilai_buku_juli"] == "1",
                NilaiBukuAgustus = data["show_table_column"]["nilai_buku_agustus"] == "1",
                NilaiBukuSeptember = data["show_table_column"]["nilai_buku_september"] == "1",
                NilaiBukuOktober = data["show_table_column"]["nilai_buku_oktober"] == "1",
                NilaiBukuNovember = data["show_table_column"]["nilai_buku_november"] == "1",
                NilaiBukuDesember = data["show_table_column"]["nilai_buku_desember"] == "1"
            };

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog");
        }
    }
}
