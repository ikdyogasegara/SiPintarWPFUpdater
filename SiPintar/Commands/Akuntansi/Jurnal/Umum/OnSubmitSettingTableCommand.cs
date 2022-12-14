using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(UmumViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\jurnal_umum_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["nomor_bukti"] = CheckValue(param, "NomorBukti");
                data["show_table_column"]["uraian"] = CheckValue(param, "Uraian");
                data["show_table_column"]["jumlah"] = CheckValue(param, "Jumlah");
                data["show_table_column"]["tanggal_transaksi"] = CheckValue(param, "TanggalTransaksi");

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
