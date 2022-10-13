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


namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnSubmitSettingTableFormCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableFormCommand(InteraksiPenyusutanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null && !_isTest)
                return;

            string successMessage = null;
            string errorMessage = null;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_penyusutan_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["AkunAktiva"] = CheckValue(param, "AkunAktiva");
                data["show_table_column"]["NamaAktiva"] = CheckValue(param, "NamaAktiva");
                data["show_table_column"]["AkunPenyusutan"] = CheckValue(param, "AkunPenyusutan");
                data["show_table_column"]["AkumulasiPenyusutan"] = CheckValue(param, "AkumulasiPenyusutan");

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

        private string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                if (errorMessage != null)
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(errorMessage); });
                else if (successMessage != null)
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
                var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(successMessage, "success");
            }
            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
