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

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
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
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_rekening_lltt_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["publish"] = CheckValue(param, "Publish");
                data["show_table_column"]["no_lltt"] = CheckValue(param, "NoLltt");
                data["show_table_column"]["no_samb"] = CheckValue(param, "NoSamb");
                data["show_table_column"]["nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["biaya"] = CheckValue(param, "Biaya");
                data["show_table_column"]["upload"] = CheckValue(param, "Upload");
                data["show_table_column"]["kode_lltt"] = CheckValue(param, "KodeLltt");
                data["show_table_column"]["tarif_lltt"] = CheckValue(param, "TarifLltt");
                data["show_table_column"]["kode_rayon"] = CheckValue(param, "KodeRayon");
                data["show_table_column"]["rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["lunas"] = CheckValue(param, "Lunas");
                data["show_table_column"]["tgl_bayar"] = CheckValue(param, "TglBayar");
                data["show_table_column"]["kode_wilayah"] = CheckValue(param, "KodeWilayah");
                data["show_table_column"]["wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["waktu_publish"] = CheckValue(param, "WaktuPublish");
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
