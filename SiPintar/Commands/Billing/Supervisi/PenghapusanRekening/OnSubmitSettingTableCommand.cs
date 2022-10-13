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

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;
        private readonly string _testParam;

        public OnSubmitSettingTableCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false, string testParam = null)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _testParam = testParam;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string successMessage = null;
            string ErrorMessage = null;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_penghapusan_rekening_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["no_samb"] = CheckValue(param, "NoSamb");
                data["show_table_column"]["golongan"] = CheckValue(param, "Golongan");
                data["show_table_column"]["rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");

                data["show_table_column"]["stan_lalu"] = CheckValue(param, "StanLalu");
                data["show_table_column"]["stan_skrg"] = CheckValue(param, "StanSkrg");
                data["show_table_column"]["stan_angkat"] = CheckValue(param, "StanAngkat");
                data["show_table_column"]["pakai"] = CheckValue(param, "Pakai");
                data["show_table_column"]["biaya_pemakaian"] = CheckValue(param, "BiayaPemakaian");
                data["show_table_column"]["administrasi"] = CheckValue(param, "Administrasi");
                data["show_table_column"]["pemeliharaan"] = CheckValue(param, "Pemeliharaan");
                data["show_table_column"]["retribusi"] = CheckValue(param, "Retribusi");
                data["show_table_column"]["pemeliharaan_lain"] = CheckValue(param, "PemeliharaanLain");
                data["show_table_column"]["administrasi_lain"] = CheckValue(param, "AdministrasiLain");
                data["show_table_column"]["retribusi_lain"] = CheckValue(param, "RetribusiLain");
                data["show_table_column"]["air_limbah"] = CheckValue(param, "AirLimbah");
                data["show_table_column"]["denda_pakai0"] = CheckValue(param, "DendaPakai0");
                data["show_table_column"]["pelayanan"] = CheckValue(param, "Pelayanan");
                data["show_table_column"]["ppn"] = CheckValue(param, "Ppn");
                data["show_table_column"]["meterai"] = CheckValue(param, "Meterai");
                data["show_table_column"]["rekair"] = CheckValue(param, "Rekair");

                if (_testParam != null)
                {
                    data["show_table_column"][_testParam] = CheckValue(param, "ErrorTest");
                }

                parser.WriteFile(configIni, data);

                successMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(successMessage, ErrorMessage);

            await Task.FromResult(_isTest);
        }

        private string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
                }
                else if (successMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(successMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");

            _viewModel.OnFilterCommand.Execute(null);
        }
    }
}
