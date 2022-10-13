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

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnSubmitSettingTableFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableFormCommand(InteraksiLayananViewModel viewModel, bool isTest = false)
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
                string ConfigIni = "";
                var parser = new IniFileParser.IniFileParser();
                IniData data;
                switch (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key)
                {
                    case 0:
                        {
                            ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_air_config.ini";

                            data = parser.ReadFile(ConfigIni);

                            var param = (Dictionary<string, bool?>)parameter;

                            data["show_table_column"]["KodeWilayah"] = CheckValue(param, "KodeWilayah");
                            data["show_table_column"]["NamaWilayah"] = CheckValue(param, "NamaWilayah");
                            data["show_table_column"]["KodeGolongan"] = CheckValue(param, "KodeGolongan");
                            data["show_table_column"]["NamaGolongan"] = CheckValue(param, "NamaGolongan");
                            data["show_table_column"]["KodePerkiraan3Debet"] = CheckValue(param, "KodePerkiraan3Debet");
                            data["show_table_column"]["NamaPerkiraan3Debet"] = CheckValue(param, "NamaPerkiraan3Debet");
                            data["show_table_column"]["KodePerkiraan3Kredit"] = CheckValue(param, "KodePerkiraan3Kredit");
                            data["show_table_column"]["NamaPerkiraan3Kredit"] = CheckValue(param, "NamaPerkiraan3Kredit");
                            data["show_table_column"]["FlagPembentukRekair"] = CheckValue(param, "FlagPembentukRekair");
                            data["show_table_column"]["Keterangan"] = CheckValue(param, "Keterangan");
                        }
                        break;
                    default:
                        {
                            ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_non_air_config.ini";

                            data = parser.ReadFile(ConfigIni);

                            var param = (Dictionary<string, bool?>)parameter;

                            data["show_table_column"]["KodeWilayah"] = CheckValue(param, "KodeWilayah");
                            data["show_table_column"]["NamaWilayah"] = CheckValue(param, "NamaWilayah");
                            data["show_table_column"]["KodeGolongan"] = CheckValue(param, "KodeGolongan");
                            data["show_table_column"]["NamaGolongan"] = CheckValue(param, "NamaGolongan");
                            data["show_table_column"]["KodePerkiraan3"] = CheckValue(param, "KodePerkiraan3");
                            data["show_table_column"]["NamaPerkiraan3"] = CheckValue(param, "NamaPerkiraan3");
                            data["show_table_column"]["KodeJenisNonAir"] = CheckValue(param, "KodeJenisNonAir");
                            data["show_table_column"]["NamaJenisNonAir"] = CheckValue(param, "NamaJenisNonAir");
                            data["show_table_column"]["Keterangan"] = CheckValue(param, "Keterangan");
                        }
                        break;


                }


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
                var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(SuccessMessage, "success");
            }
            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
