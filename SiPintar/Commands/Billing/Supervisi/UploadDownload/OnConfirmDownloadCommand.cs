using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.UploadDownload;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.UploadDownload
{
    public class OnConfirmDownloadCommand : AsyncCommandBase
    {
        private readonly UploadDownloadViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnConfirmDownloadCommand(UploadDownloadViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            ShowLoadingDialog();

            RestApiResponse ResponsePelangganAir = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-pelanggan-air", null));
            RestApiResponse ResponsePelangganLimbah = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-pelanggan-limbah", null));
            RestApiResponse ResponsePelangganLltt = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-pelanggan-lltt", null));
            RestApiResponse ResponseAir = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-transaksi-air", null));
            RestApiResponse ResponseNonAir = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-transaksi-non-air", null));
            RestApiResponse ResponseLimbah = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-transaksi-limbah", null));
            RestApiResponse ResponseLltt = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/download-transaksi-lltt", null));

            var Result = new DownloadResult()
            {
                BayarRekeningAir = 0,
                BatalRekeningAir = 0,
                BayarRekeningNonAir = 0,
                BatalRekeningNonAir = 0,
                BayarRekeningLimbah = 0,
                BatalRekeningLimbah = 0,
                BayarRekeningLltt = 0,
                BatalRekeningLltt = 0,
            };
            if (!ResponseAir.IsError)
            {
                var result = ResponseAir.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<List<dynamic>>();
                    if (data.Any())
                    {
                        Result.BayarRekeningAir = (int)data[0].bayar;
                        Result.BatalRekeningAir = (int)data[0].batal;
                    }
                }
            }
            if (!ResponseNonAir.IsError)
            {
                var result = ResponseNonAir.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<List<dynamic>>();
                    if (data.Any())
                    {
                        Result.BayarRekeningNonAir = (int)data[0].bayar;
                        Result.BatalRekeningNonAir = (int)data[0].batal;
                    }
                }
            }
            if (!ResponseLimbah.IsError)
            {
                var result = ResponseLimbah.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<List<dynamic>>();
                    if (data.Any())
                    {
                        Result.BayarRekeningLimbah = (int)data[0].bayar;
                        Result.BatalRekeningLimbah = (int)data[0].batal;
                    }
                }
            }
            if (!ResponseLltt.IsError)
            {
                var result = ResponseLltt.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<List<dynamic>>();
                    if (data.Any())
                    {
                        Result.BayarRekeningLltt = (int)data[0].bayar;
                        Result.BatalRekeningLltt = (int)data[0].batal;
                    }
                }
            }

            ShowDialogResult(Result);
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoadingDialog()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DialogHost.Close("BillingRootDialog");
                    _ = DialogHost.Show(new GlobalLoadingDialog("Mohon tunggu...", "Download data", "sedang berlangsung"), "BillingRootDialog");
                });
        }

        [ExcludeFromCodeCoverage]
        public void ShowDialogResult(DownloadResult Result)
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DialogHost.Close("BillingRootDialog");
                    _ = DialogHost.Show(new DialogFormView(Result), "BillingRootDialog");
                });
        }
    }

    public class DownloadResult
    {
        public int BayarRekeningAir { get; set; }
        public int BatalRekeningAir { get; set; }
        public int BayarRekeningNonAir { get; set; }
        public int BatalRekeningNonAir { get; set; }
        public int BayarRekeningLimbah { get; set; }
        public int BatalRekeningLimbah { get; set; }
        public int BayarRekeningLltt { get; set; }
        public int BatalRekeningLltt { get; set; }
    }
}
