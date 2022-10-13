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
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null && !_isTest)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            try
            {
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\atribut_tarif_tangki_air_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["KodeTarif"] = CheckValue(param, "KodeTarif");
                data["show_table_column"]["Kategori"] = CheckValue(param, "Kategori");
                data["show_table_column"]["NamaTarif"] = CheckValue(param, "NamaTarif");
                data["show_table_column"]["BiayaAir"] = CheckValue(param, "BiayaAir");
                data["show_table_column"]["BiayaAdministrasi"] = CheckValue(param, "BiayaAdministrasi");
                data["show_table_column"]["BiayaOperasional"] = CheckValue(param, "BiayaOperasional");
                data["show_table_column"]["BiayaTransport"] = CheckValue(param, "BiayaTransport");
                data["show_table_column"]["Ppn"] = CheckValue(param, "Ppn");

                parser.WriteFile(ConfigIni, data);

                SuccessMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(SuccessMessage, ErrorMessage);

            await Task.FromResult(_isTest);
        }

        private string CheckValue(Dictionary<string, bool?> param, string Key) => param[Key] != null && (bool)param[Key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                if (ErrorMessage != null)
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
                else if (SuccessMessage != null)
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(SuccessMessage); });
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            if (!_isTest) { _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error")); }
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            if (!_isTest)
            {
                var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(SuccessMessage, "success");
            }
            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
