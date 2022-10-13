using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(PeriodeViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_periode_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["bulan"] = CheckValue(param, "Bulan");
                data["show_table_column"]["tgl_mulai_tagih"] = CheckValue(param, "TglMulaiTagih");
                data["show_table_column"]["pelanggan_air"] = CheckValue(param, "PelangganAir");
                data["show_table_column"]["pelanggan_limbah"] = CheckValue(param, "PelangganLimbah");
                data["show_table_column"]["pelanggan_l2t2"] = CheckValue(param, "PelangganL2T2");
                data["show_table_column"]["rekening_air"] = CheckValue(param, "RekeningAir");
                data["show_table_column"]["rekening_limbah"] = CheckValue(param, "RekeningLimbah");
                data["show_table_column"]["rekening_l2t2"] = CheckValue(param, "RekeningL2T2");
                data["show_table_column"]["status"] = CheckValue(param, "Status");
                data["show_table_column"]["jumlahpakaiair"] = CheckValue(param, "JumlahPakaiAir");
                data["show_table_column"]["jumlahkelainan"] = CheckValue(param, "JumlahKelainan");
                data["show_table_column"]["jumlahtaksir"] = CheckValue(param, "JumlahTaksir");
                data["show_table_column"]["pelangganairpublish"] = CheckValue(param, "PelangganAirPublish");
                data["show_table_column"]["pelangganlimbahpublish"] = CheckValue(param, "PelangganLimbahPublish");
                data["show_table_column"]["pelangganllttpublish"] = CheckValue(param, "PelangganLlttPublish");

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
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
