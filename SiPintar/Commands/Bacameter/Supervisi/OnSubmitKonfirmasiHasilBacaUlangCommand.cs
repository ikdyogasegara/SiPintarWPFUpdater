using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Serilog;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitKonfirmasiHasilBacaUlangCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        private string SuccessMessage;
        private new string ErrorMessage;

        public OnSubmitKonfirmasiHasilBacaUlangCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            SuccessMessage = null;
            ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

            int[] IdPelangganAir = new int[] { (int)_viewModel.SelectedData.IdPelangganAir };

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir },
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "Konfirmasi", true }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-air-konfirmasi-hasil-baca-ulang", null, body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("bacameter"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BacameterRootDialog",
                    App.OpenedWindow["bacameter"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        #region Change Attachment
        [ExcludeFromCodeCoverage]
        private void ChangeAttachment()
        {
            if (SuccessMessage == null)
                return;

            try
            {
                string remoteDirectory = App.Current.Resources["IsDLokasiFotoMeter"]?.ToString();
                string tempDirectory = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

                string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                var UrlThumbnailFotoStanLama = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}.jpg");
                var UrlThumbnailFotoStanBaru = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}U.jpg");
                var UrlFotoStanLama = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}.jpg");
                var UrlFotoStanBaru = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}U.jpg");

                var UrlThumbnailFotoRumahLama = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}R.jpg");
                var UrlThumbnailFotoRumahBaru = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}RU.jpg");
                var UrlFotoRumahLama = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}R.jpg");
                var UrlFotoRumahBaru = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}RU.jpg");

                var UrlVideoLama = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}V.mp4");
                var UrlVideoBaru = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}VU.mp4");

                var TempFotoStanLama = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}.jpg");
                var TempFotoStanBaru = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}U.jpg");
                var TempFotoRumahLama = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}R.jpg");
                var TempFotoRumahBaru = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}RU.jpg");
                var TempVideoLama = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}V.mp4");
                var TempVideoBaru = Path.Combine(tempDirectory, $"{_viewModel.SelectedData.NoSamb}VU.mp4");

                UrlThumbnailFotoStanLama = Path.Combine(remoteDirectory, UrlThumbnailFotoStanLama);
                UrlThumbnailFotoStanBaru = Path.Combine(remoteDirectory, UrlThumbnailFotoStanBaru);
                UrlFotoStanLama = Path.Combine(remoteDirectory, UrlFotoStanLama);
                UrlFotoStanBaru = Path.Combine(remoteDirectory, UrlFotoStanBaru);

                UrlThumbnailFotoRumahLama = Path.Combine(remoteDirectory, UrlThumbnailFotoRumahLama);
                UrlThumbnailFotoRumahBaru = Path.Combine(remoteDirectory, UrlThumbnailFotoRumahBaru);
                UrlFotoRumahLama = Path.Combine(remoteDirectory, UrlFotoRumahLama);
                UrlFotoRumahBaru = Path.Combine(remoteDirectory, UrlFotoRumahBaru);

                UrlVideoLama = Path.Combine(remoteDirectory, UrlVideoLama);
                UrlVideoBaru = Path.Combine(remoteDirectory, UrlVideoBaru);

                if (File.Exists(UrlThumbnailFotoStanLama) && File.Exists(UrlThumbnailFotoStanBaru))
                {
                    File.Delete(UrlThumbnailFotoStanLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlThumbnailFotoStanLama, UrlThumbnailFotoStanLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlThumbnailFotoStanBaru, UrlThumbnailFotoStanBaru.Replace("U.jpg", ".jpg"));
                }

                if (File.Exists(UrlFotoStanLama) && File.Exists(UrlFotoStanBaru))
                {
                    File.Delete(UrlFotoStanLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlFotoStanLama, UrlFotoStanLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlFotoStanBaru, UrlFotoStanBaru.Replace("U.jpg", ".jpg"));
                }

                if (File.Exists(UrlThumbnailFotoRumahLama) && File.Exists(UrlThumbnailFotoRumahBaru))
                {
                    File.Delete(UrlThumbnailFotoRumahLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlThumbnailFotoRumahLama, UrlThumbnailFotoRumahLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlThumbnailFotoRumahBaru, UrlThumbnailFotoRumahBaru.Replace("RU.jpg", "R.jpg"));
                }

                if (File.Exists(UrlFotoRumahLama) && File.Exists(UrlFotoRumahBaru))
                {
                    File.Delete(UrlFotoRumahLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlFotoRumahLama, UrlFotoRumahLama.Replace(".jpg", "_backup.jpg"));
                    File.Move(UrlFotoRumahBaru, UrlFotoRumahBaru.Replace("RU.jpg", "R.jpg"));
                }

                if (File.Exists(UrlVideoLama) && File.Exists(UrlVideoBaru))
                {
                    File.Delete(UrlVideoLama.Replace(".mp4", "_backup.mp4"));
                    File.Move(UrlVideoLama, UrlVideoLama.Replace(".mp4", "_backup.mp4"));
                    File.Move(UrlVideoBaru, UrlVideoBaru.Replace("VU.mp4", "V.mp4"));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Log.Logger.Error(e.ToString());
            }
        }
        #endregion
    }
}
