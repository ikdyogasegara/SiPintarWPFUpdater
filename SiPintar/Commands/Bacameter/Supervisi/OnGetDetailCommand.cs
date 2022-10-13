using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnGetDetailCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnGetDetailCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null || _viewModel.SelectedPeriode == null)
                return;

            if (_viewModel.HasFotoStan)
                _viewModel.HasFotoStan = false;

            if (_viewModel.HasFotoRumah)
                _viewModel.HasFotoRumah = false;

            if (_viewModel.HasVideo)
                _viewModel.HasVideo = false;

            var IdPelanggan = _viewModel.SelectedData.IdPelangganAir;

            if (!_isTest)
                await Task.Delay(2000);

            if (IdPelanggan != _viewModel.SelectedData.IdPelangganAir)
                return;

            string ErrorMessage = null;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir },
                { "IdPeriode", _viewModel.SelectedPeriode.IdPeriode }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/rekening-air-detail", param));

            if (!(parameter is bool))
                _viewModel.DetailData = new RekeningAirDetailDto();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var data = Result.Data.ToObject<ObservableCollection<RekeningAirDetailDto>>();
                    if (data.Count > 0)
                        _viewModel.DetailData = data[0];
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            GetFotoStan();
            GetVideo();
            GetFotoRumah();

            ShowIfError(ErrorMessage);
        }

        private void ShowIfError(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

        [ExcludeFromCodeCoverage]
        private void GetFotoStan()
        {
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.KodePeriode != null && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.NoSamb))
            {
                string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}.jpg");

                if (!_isTest)
                    _viewModel.GetFotoStanCommand.Execute(UrlFoto);
            }
        }

        [ExcludeFromCodeCoverage]
        private void GetVideo()
        {
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.KodePeriode != null && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.NoSamb))
            {
                string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                var UrlFoto = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}V.mp4");

                if (!_isTest)
                    _viewModel.GetVideoCommand.Execute(UrlFoto);
            }
        }

        [ExcludeFromCodeCoverage]
        private void GetFotoRumah()
        {
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.KodePeriode != null && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.NoSamb))
            {
                string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}R.jpg");

                if (!_isTest)
                    _viewModel.GetFotoRumahCommand.Execute(UrlFoto);
            }
        }
    }
}
