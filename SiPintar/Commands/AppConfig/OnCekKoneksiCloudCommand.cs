using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Helpers;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.AppConfig
{
    [ExcludeFromCodeCoverage]
    public class OnCekKoneksiCloudCommand : AsyncCommandBase
    {
        private readonly AppConfigViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isReport;

        public OnCekKoneksiCloudCommand(AppConfigViewModel viewModel, bool isReport = false, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _isReport = isReport;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (string.IsNullOrEmpty(_isReport ? _viewModel.ApiCloudReportUrl : _viewModel.ApiCloudUrl))
                return;

            bool IsConnectionOk = false;

            DialogHelper.ShowLoading(_isTest, "ApplicationConfigViewDialog");

            try
            {
                var Url = _isReport ? _viewModel.ApiCloudReportUrl : _viewModel.ApiCloudUrl;
                if (Url[^1] == '/')
                    Url = Url[0..^1];

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"{Url}/api/pengaturan");

                IsConnectionOk = response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Debug.WriteLine("error cek koneksi: " + e);
                Log.Logger.Error(e.ToString());
            }

            Application.Current.Dispatcher.Invoke(delegate { DialogHost.Close("ApplicationConfigViewDialog"); });

            if (!IsConnectionOk)
                ShowError();
            else
                ShowSuccess();

            await Task.FromResult(_isTest);
        }

        private void ShowSuccess()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    $"Koneksi Cloud Api {(_isReport ? "Report " : "")}Berhasil",
                    "Anda dapat menyimpan pengaturan API URL ini.",
                    "success",
                    "Oke",
                    false
                ), "ApplicationConfigViewDialog");
        }

        private void ShowError()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    $"Koneksi Cloud Api {(_isReport ? "Report " : "")}Gagal",
                    "Pastikan url telah sesuai, silakan hubungi tim teknis jika mengalami kendala.",
                    "error",
                    "Oke",
                    false
                ), "ApplicationConfigViewDialog");
        }
    }
}
