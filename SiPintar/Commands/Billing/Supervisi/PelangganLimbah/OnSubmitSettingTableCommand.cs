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
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(PelangganLimbahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            try
            {
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_pelanggan_limbah_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["no_limbah"] = CheckValue(param, "NoLimbah");
                data["show_table_column"]["no_samb"] = CheckValue(param, "NoSamb");
                data["show_table_column"]["nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["kode_limbah"] = CheckValue(param, "KodeLimbah");
                data["show_table_column"]["tarif_limbah"] = CheckValue(param, "TarifLimbah");
                data["show_table_column"]["rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["kolektif"] = CheckValue(param, "Kolektif");
                data["show_table_column"]["no_telp"] = CheckValue(param, "NoTelp");
                data["show_table_column"]["no_hp"] = CheckValue(param, "NoHp");
                data["show_table_column"]["email"] = CheckValue(param, "Email");
                data["show_table_column"]["ktp"] = CheckValue(param, "Ktp");
                data["show_table_column"]["keterangan"] = CheckValue(param, "Keterangan");
                data["show_table_column"]["flag"] = CheckValue(param, "Flag");
                data["show_table_column"]["status"] = CheckValue(param, "Status");

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
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(SuccessMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
