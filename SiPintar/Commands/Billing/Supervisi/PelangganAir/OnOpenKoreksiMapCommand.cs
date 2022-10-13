using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenKoreksiMapCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKoreksiMapCommand(PelangganAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await OpenDialogAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public async Task OpenDialogAsync()
        {
            if (!_isTest)
            {
                bool IsConnectionOk = false;

                try
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://google.com");
                    request.Timeout = 5000;
                    request.Credentials = CredentialCache.DefaultNetworkCredentials;
                    var response = (HttpWebResponse)await request.GetResponseAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                        IsConnectionOk = true;
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error);
                }

                if (IsConnectionOk)
                    _ = DialogHost.Show(new KoreksiMapPelangganView(_viewModel), "SupervisiPelangganDialog");
                else
                    _ = DialogHost.Show(new DialogGlobalView(
                        "Tidak Ada Koneksi",
                        "Pengaturan Titik Lokasi pada Map harus terhubung ke internet.",
                        "error",
                        "OK",
                        false,
                        "billing"
                    ), "SupervisiPelangganDialog");
            }
        }
    }
}
