using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Penyusutan;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(DataAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string successMessage = null;
            string errorMessage = null;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\penyusutan_data_aktiva_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["uraian"] = CheckValue(param, "Uraian");
                data["show_table_column"]["tahun"] = CheckValue(param, "Tahun");
                data["show_table_column"]["lokasi"] = CheckValue(param, "Lokasi");
                data["show_table_column"]["asal"] = CheckValue(param, "Asal");
                data["show_table_column"]["tanggal"] = CheckValue(param, "Tanggal");
                data["show_table_column"]["nilai"] = CheckValue(param, "Nilai");
                data["show_table_column"]["mutasi"] = CheckValue(param, "Mutasi");
                data["show_table_column"]["perolehan"] = CheckValue(param, "Perolehan");
                data["show_table_column"]["jml_tahun"] = CheckValue(param, "JmlTahun");
                data["show_table_column"]["persen"] = CheckValue(param, "Persen");
                data["show_table_column"]["nilai_buku_lalu"] = CheckValue(param, "NilaiBukuLalu");
                data["show_table_column"]["akun_peny_lalu"] = CheckValue(param, "AkunPenyLalu");
                data["show_table_column"]["penyusutan_januari"] = CheckValue(param, "PenyusutanJanuari");
                data["show_table_column"]["penyusutan_februari"] = CheckValue(param, "PenyusutanFebruari");
                data["show_table_column"]["penyusutan_maret"] = CheckValue(param, "PenyusutanMaret");
                data["show_table_column"]["penyusutan_april"] = CheckValue(param, "PenyusutanApril");
                data["show_table_column"]["penyusutan_mei"] = CheckValue(param, "PenyusutanMei");
                data["show_table_column"]["penyusutan_juni"] = CheckValue(param, "PenyusutanJuni");
                data["show_table_column"]["penyusutan_juli"] = CheckValue(param, "PenyusutanJuli");
                data["show_table_column"]["penyusutan_agustus"] = CheckValue(param, "PenyusutanAgustus");
                data["show_table_column"]["penyusutan_september"] = CheckValue(param, "PenyusutanSeptember");
                data["show_table_column"]["penyusutan_oktober"] = CheckValue(param, "PenyusutanOktober");
                data["show_table_column"]["penyusutan_november"] = CheckValue(param, "PenyusutanNovember");
                data["show_table_column"]["penyusutan_desember"] = CheckValue(param, "PenyusutanDesember");
                data["show_table_column"]["akun_penyusutan_januari"] = CheckValue(param, "AkunPenyusutanJanuari");
                data["show_table_column"]["akun_penyusutan_februari"] = CheckValue(param, "AkunPenyusutanFebruari");
                data["show_table_column"]["akun_penyusutan_maret"] = CheckValue(param, "AkunPenyusutanMaret");
                data["show_table_column"]["akun_penyusutan_april"] = CheckValue(param, "AkunPenyusutanApril");
                data["show_table_column"]["akun_penyusutan_mei"] = CheckValue(param, "AkunPenyusutanMei");
                data["show_table_column"]["akun_penyusutan_juni"] = CheckValue(param, "AkunPenyusutanJuni");
                data["show_table_column"]["akun_penyusutan_juli"] = CheckValue(param, "AkunPenyusutanJuli");
                data["show_table_column"]["akun_penyusutan_agustus"] = CheckValue(param, "AkunPenyusutanAgustus");
                data["show_table_column"]["akun_penyusutan_september"] = CheckValue(param, "AkunPenyusutanSeptember");
                data["show_table_column"]["akun_penyusutan_oktober"] = CheckValue(param, "AkunPenyusutanOktober");
                data["show_table_column"]["akun_penyusutan_november"] = CheckValue(param, "AkunPenyusutanNovember");
                data["show_table_column"]["akun_penyusutan_desember"] = CheckValue(param, "AkunPenyusutanDesember");
                data["show_table_column"]["nilai_buku_januari"] = CheckValue(param, "NilaiBukuJanuari");
                data["show_table_column"]["nilai_buku_februari"] = CheckValue(param, "NilaiBukuFebruari");
                data["show_table_column"]["nilai_buku_maret"] = CheckValue(param, "NilaiBukuMaret");
                data["show_table_column"]["nilai_buku_april"] = CheckValue(param, "NilaiBukuApril");
                data["show_table_column"]["nilai_buku_mei"] = CheckValue(param, "NilaiBukuMei");
                data["show_table_column"]["nilai_buku_juni"] = CheckValue(param, "NilaiBukuJuni");
                data["show_table_column"]["nilai_buku_juli"] = CheckValue(param, "NilaiBukuJuli");
                data["show_table_column"]["nilai_buku_agustus"] = CheckValue(param, "NilaiBukuAgustus");
                data["show_table_column"]["nilai_buku_september"] = CheckValue(param, "NilaiBukuSeptember");
                data["show_table_column"]["nilai_buku_oktober"] = CheckValue(param, "NilaiBukuOktober");
                data["show_table_column"]["nilai_buku_november"] = CheckValue(param, "NilaiBukuNovember");
                data["show_table_column"]["nilai_buku_desember"] = CheckValue(param, "NilaiBukuDesember");

                parser.WriteFile(configIni, data);

                successMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                errorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(successMessage, errorMessage);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private static string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                if (errorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(errorMessage); });
                }
                else if (successMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(successMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string errorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "AkuntansiRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");


            _viewModel.OnRefreshCommand.Execute(null);

        }
    }
}
