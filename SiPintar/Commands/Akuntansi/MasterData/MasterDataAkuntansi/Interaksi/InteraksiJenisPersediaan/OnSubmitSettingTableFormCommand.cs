using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    public class OnSubmitSettingTableFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableFormCommand(InteraksiJenisPersediaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null && !_isTest)
                return;

            _viewModel.Parent.IsLoadingForm = true;

            var successMessage = string.Empty;
            var errorMessage = string.Empty;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_jenis_persediaan_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = parameter! as Dictionary<string, bool?>;

                data["show_table_column"]["KodeJenisBarang"] = CheckValue(param!, "KodeJenisBarang");
                data["show_table_column"]["NamaJenisBarang"] = CheckValue(param!, "NamaJenisBarang");
                data["show_table_column"]["KodeKeperluan"] = CheckValue(param!, "KodeKeperluan");
                data["show_table_column"]["Keperluan"] = CheckValue(param!, "Keperluan");
                data["show_table_column"]["KodeDebet"] = CheckValue(param!, "KodeDebet");
                data["show_table_column"]["NamaDebet"] = CheckValue(param!, "NamaDebet");
                data["show_table_column"]["KodeKredit"] = CheckValue(param!, "KodeKredit");
                data["show_table_column"]["NamaKredit"] = CheckValue(param!, "NamaKredit");

                parser.WriteFile(configIni, data);

                successMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                errorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(successMessage, errorMessage);

            _viewModel.Parent.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private static string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key]! ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(errorMessage); });
                else if (!string.IsNullOrWhiteSpace(successMessage))
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(successMessage); });
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string errorMessage)
        {
            if (!_isTest) { _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error")); }
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            if (!_isTest)
            {
                var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)!;
                if (mainView != null)
                    mainView.ShowSnackbar(successMessage, "success");
            }
            _ = (_viewModel.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
        }
    }
}
