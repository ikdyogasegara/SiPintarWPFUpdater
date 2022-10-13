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

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null) return;

            _viewModel.IsLoadingForm = true;

            var successMessage = string.Empty;
            var errorMessage = string.Empty;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\posting_keuangan_penerimaanlainnya_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["notrans"] = CheckValue(param, "NoTrans");
                data["show_table_column"]["kode_wilayah"] = CheckValue(param, "KodeWilayah");
                data["show_table_column"]["nama_wilayah"] = CheckValue(param, "NamaWilayah");
                data["show_table_column"]["nama_debet"] = CheckValue(param, "NamaDebet");
                data["show_table_column"]["uraian"] = CheckValue(param, "Uraian");
                data["show_table_column"]["waktu_input"] = CheckValue(param, "WaktuInput");
                data["show_table_column"]["jumlah_nominal"] = CheckValue(param, "JumlahNominal");
                data["show_table_column"]["kode_kredit"] = CheckValue(param, "KodeKredit");
                data["show_table_column"]["nama_kredit"] = CheckValue(param, "NamaKredit");

                parser.WriteFile(configIni, data);

                successMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                errorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(successMessage, errorMessage);

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private static string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key]! ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(errorMessage); });
                }
                else if (!string.IsNullOrWhiteSpace(successMessage))
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(successMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private static void ShowError(string errorMessage) => _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "AkuntansiRootDialog");

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)!;
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");

            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
