using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(PengeluaranLainnyaViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\posting_keuangan_pengeluaranlainnya_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;


                data["show_table_column"]["NomorTransaksi"] = CheckValue(param, "NomorTransaksi");
                data["show_table_column"]["KodeWilayah"] = CheckValue(param, "KodeWilayah");
                data["show_table_column"]["NamaWilayah"] = CheckValue(param, "NamaWilayah");
                data["show_table_column"]["KodePerkiraanDebet"] = CheckValue(param, "KodePerkiraanDebet");
                data["show_table_column"]["NamaPerkiraanDebet"] = CheckValue(param, "NamaPerkiraanDebet");
                data["show_table_column"]["KodePerkiraanKredit"] = CheckValue(param, "KodePerkiraanKredit");
                data["show_table_column"]["NamaPerkiraanKredit"] = CheckValue(param, "NamaPerkiraanKredit");
                data["show_table_column"]["Uraian"] = CheckValue(param, "Uraian");
                data["show_table_column"]["JumlahNominal"] = CheckValue(param, "JumlahNominal");
                data["show_table_column"]["TanggalTerima"] = CheckValue(param, "TanggalTerima");

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
