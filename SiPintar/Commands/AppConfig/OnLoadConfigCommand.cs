using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels;

namespace SiPintar.Commands.AppConfig
{
    [ExcludeFromCodeCoverage]
    public class OnLoadConfigCommand : AsyncCommandBase
    {
        private readonly string _configEnvPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        private readonly AppConfigViewModel _viewModel;

        public OnLoadConfigCommand(AppConfigViewModel viewModel)
        {
            _viewModel = viewModel;

        }

        public override async Task ExecuteAsync(object parameter)
        {
            LoadConfigEnv();
            LoadOtherConfig();

            await Task.FromResult(true);
        }

        private void LoadConfigEnv()
        {
            DotNetEnv.Env.Load(_configEnvPath);

            // set API URL
            _viewModel.ApiCloudUrl = DotNetEnv.Env.GetString("CLOUD_API_URL");
            _viewModel.ApiLocalUrl = DotNetEnv.Env.GetString("LOCAL_API_URL");
            _viewModel.ApiLoket = DotNetEnv.Env.GetString("LOKET_MODULE_API") ?? "CLOUD";
            _viewModel.ApiBilling = DotNetEnv.Env.GetString("BILLING_MODULE_API") ?? "CLOUD";
            _viewModel.ApiBacameter = DotNetEnv.Env.GetString("BACAMETER_MODULE_API") ?? "CLOUD";
            _viewModel.ApiHublang = DotNetEnv.Env.GetString("HUBLANG_MODULE_API") ?? "CLOUD";
            _viewModel.ApiPerencanaan = DotNetEnv.Env.GetString("PERENCANAAN_MODULE_API") ?? "CLOUD";
            _viewModel.ApiDistribusi = DotNetEnv.Env.GetString("DISTRIBUSI_MODULE_API") ?? "CLOUD";
            _viewModel.ApiAkuntansiKeuangan = DotNetEnv.Env.GetString("AKUNTANSIKEUANGAN_MODULE_API") ?? "CLOUD";

            // set API URL Report
            _viewModel.CheckReportSameSetting = !string.IsNullOrWhiteSpace(DotNetEnv.Env.GetString("CHECK_REPORT_SAMESETTING")) && (DotNetEnv.Env.GetString("CHECK_REPORT_SAMESETTING").ToLower() == "true");
            _viewModel.ApiCloudReportUrl = DotNetEnv.Env.GetString("CLOUD_REPORT_API_URL");
            _viewModel.ApiLocalReportUrl = DotNetEnv.Env.GetString("LOCAL_REPORT_API_URL");
            _viewModel.ApiLoketReport = DotNetEnv.Env.GetString("LOKET_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiBillingReport = DotNetEnv.Env.GetString("BILLING_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiBacameterReport = DotNetEnv.Env.GetString("BACAMETER_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiHublangReport = DotNetEnv.Env.GetString("HUBLANG_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiPerencanaanReport = DotNetEnv.Env.GetString("PERENCANAAN_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiDistribusiReport = DotNetEnv.Env.GetString("DISTRIBUSI_MODULE_REPORT_API") ?? "CLOUD";
            _viewModel.ApiAkuntansiKeuanganReport = DotNetEnv.Env.GetString("AKUNTANSIKEUANGAN_MODULE_REPORT_API") ?? "CLOUD";

            _viewModel.ReportAppPath = DotNetEnv.Env.GetString("REPORT_PATH");

            // set App Version
            _viewModel.AppVersion = Application.Current.Resources["Versi"].ToString();

            AppSetting.ElasticSearchUrl = DotNetEnv.Env.GetString("ELASTICSEARCH_URL");
        }

        private void LoadOtherConfig()
        {
            // if other config is served..
        }
    }
}
