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
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;


namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    public class OnSubmitSettingTableFormCommand : AsyncCommandBase
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _invokeException;

        public OnSubmitSettingTableFormCommand(PersentasePenyusutanAktivaViewModel viewModel, bool isTest = false, bool invokeException = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _invokeException = invokeException;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string? successMessage = null;
            string? errorMessage = null;

            try
            {
                if (_invokeException)
                    throw new NotImplementedException("Test Invoke");

                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_persentase_penyusutan_aktiva_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["GolAktiva"] = CheckValue(param, "GolAktiva");
                data["show_table_column"]["NamaGolAktiva"] = CheckValue(param, "NamaGolAktiva");
                data["show_table_column"]["JumlahTahun"] = CheckValue(param, "JumlahTahun");
                data["show_table_column"]["Persen"] = CheckValue(param, "Persen");
                data["show_table_column"]["TipeAktiva"] = CheckValue(param, "TipeAktiva");

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

        private static string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key]! ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string? successMessage, string? errorMessage)
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
            if (!_isTest && Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) is AkuntansiView mainView)
            {
                mainView.ShowSnackbar(successMessage, "success");
            }
            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
