using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.AppConfig
{
    [ExcludeFromCodeCoverage]
    public class OnSaveCommand : AsyncCommandBase
    {
        private readonly string ConfigEnvPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";

        private readonly AppConfigViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private string SuccessMessage;
        private new string ErrorMessage;

        public OnSaveCommand(AppConfigViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            SuccessMessage = null;
            ErrorMessage = null;

            SaveConfigEnv();
            SaveOtherConfig(parameter);

            _ = ShowResultAsync(SuccessMessage, ErrorMessage);

            await Task.FromResult(_isTest);
        }

        private void SaveConfigEnv()
        {
            try
            {
                var data = DotNetEnv.Env.Load(ConfigEnvPath);

                var strBuilder = new StringBuilder();
                foreach (var item in data)
                {
                    string Key = item.Key;
                    string Value = Key switch
                    {
                        "CLOUD_API_URL" => _viewModel.ApiCloudUrl,
                        "LOCAL_API_URL" => _viewModel.ApiLocalUrl,
                        "LOKET_MODULE_API" => _viewModel.ApiLoket,
                        "BILLING_MODULE_API" => _viewModel.ApiBilling,
                        "BACAMETER_MODULE_API" => _viewModel.ApiBacameter,
                        "HUBLANG_MODULE_API" => _viewModel.ApiHublang,
                        "PERENCANAAN_MODULE_API" => _viewModel.ApiPerencanaan,
                        "DISTRIBUSI_MODULE_API" => _viewModel.ApiDistribusi,
                        "AKUNTANSIKEUANGAN_MODULE_API" => _viewModel.ApiAkuntansiKeuangan,
                        "CLOUD_REPORT_API_URL" => _viewModel.CheckReportSameSetting ? _viewModel.ApiCloudUrl : _viewModel.ApiCloudReportUrl,
                        "LOCAL_REPORT_API_URL" => _viewModel.CheckReportSameSetting ? _viewModel.ApiLocalUrl : _viewModel.ApiLocalReportUrl,
                        "LOKET_MODULE_REPORT_API" => _viewModel.ApiLoketReport,
                        "BILLING_MODULE_REPORT_API" => _viewModel.ApiBillingReport,
                        "BACAMETER_MODULE_REPORT_API" => _viewModel.ApiBacameterReport,
                        "HUBLANG_MODULE_REPORT_API" => _viewModel.ApiHublangReport,
                        "PERENCANAAN_MODULE_REPORT_API" => _viewModel.ApiPerencanaanReport,
                        "DISTRIBUSI_MODULE_REPORT_API" => _viewModel.ApiDistribusiReport,
                        "AKUNTANSIKEUANGAN_MODULE_REPORT_API" => _viewModel.ApiAkuntansiKeuanganReport,
                        "REPORT_PATH" => _viewModel.ReportAppPath,
                        _ => item.Value
                    };
                    strBuilder.Append($"{Key}={Value}");
                    strBuilder.Append('\n');
                }

                string content = strBuilder.ToString();

                if (File.Exists(ConfigEnvPath.Replace("config.env", "backup_config.env")))
                    File.Delete(ConfigEnvPath.Replace("config.env", "backup_config.env"));

                File.Copy(ConfigEnvPath, ConfigEnvPath.Replace("config.env", "backup_config.env"));

                File.WriteAllText(ConfigEnvPath, content);

                _restApi.SetupConfig();
                _ = LiteDBExtension.ReSetupApiModuleAsync();

                SuccessMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }
        }

        private void SaveOtherConfig(object parameter)
        {
            _ = parameter;

            var param = new Dictionary<string, bool?>();
            _ = CheckValue(param, "IdPelanggan");
        }

        private string CheckValue(Dictionary<string, bool?> param, string Key) => param.ContainsKey(Key) && param[Key] != null && (bool)param[Key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private async Task ShowResultAsync(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                if (SuccessMessage != null)
                    await DialogHost.Show(new DialogGlobalView("Pengaturan Disimpan", SuccessMessage, "success"), "ApplicationConfigViewDialog");
                if (ErrorMessage != null)
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "warning"), "ApplicationConfigViewDialog");
            }
        }
    }
}
