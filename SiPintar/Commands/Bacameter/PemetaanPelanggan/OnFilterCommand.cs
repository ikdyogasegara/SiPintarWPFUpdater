using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.PemetaanPelanggan
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly PemetaanPelangganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();
        private new string ErrorMessage;

        public OnFilterCommand(PemetaanPelangganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            _viewModel.MarkerList = new List<MapMarkerObject>();

            switch (_viewModel.SelectedJenisPeta)
            {
                case "Peta Pelanggan":
                    await GetPetaPelangganAsync();
                    break;
                case "Peta Posisi Pelanggan & Pembacaan":
                    await GetPetaPelangganAsync();
                    await GetPetaPosisiAsync();
                    break;
                default:
                    break;
            }

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private async Task GetPetaPelangganAsync()
        {
            var param = new Dictionary<string, dynamic>();

            if (_viewModel.SelectedRayon != null)
                param.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-pelanggan-air", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var result = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();

                    var final = new List<MapMarkerObject>(_viewModel.MarkerList);
                    foreach (var item in result)
                    {
                        var IsExist = final.FirstOrDefault(i => i.IdPelanggan == item.IdPelangganAir && i.MarkerColor == "red") != null;

                        if (item.Latitude != null && item.Longitude != null && !IsExist)
                        {
                            final.Add(new MapMarkerObject()
                            {
                                IdPelanggan = item.IdPelangganAir,
                                MarkerInformation = $"LOKASI PELANGGAN\n{item.NoSamb}\n{item.Nama}\n{item.Alamat}",
                                Latitude = item.Latitude,
                                Longitude = item.Longitude,
                                MarkerColor = "red"
                            });
                        }
                    }

                    _viewModel.MarkerList = final;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowIfError();
        }

        private async Task GetPetaPosisiAsync()
        {
            var param = new Dictionary<string, dynamic>();

            if (_viewModel.SelectedRayon != null)
                param.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);
            if (_viewModel.SelectedPeriode != null)
                param.Add("IdPeriode", _viewModel.SelectedPeriode.IdPeriode);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/rekening-air", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var result = Result.Data.ToObject<ObservableCollection<RekeningAirDto>>();

                    var final = new List<MapMarkerObject>(_viewModel.MarkerList);
                    foreach (var item in result)
                    {
                        var IsExist = final.FirstOrDefault(i => i.IdPelanggan == item.IdPelangganAir && i.MarkerColor == "blue") != null;

                        if (item.Latitude != null && item.Longitude != null && !IsExist)
                        {
                            final.Add(new MapMarkerObject()
                            {
                                IdPelanggan = item.IdPelangganAir,
                                MarkerInformation = $"LOKASI PEMBACAAN\n{item.NoSamb}\n{item.Nama}\n{item.Alamat}",
                                Latitude = item.Latitude,
                                Longitude = item.Longitude,
                                MarkerColor = "blue"
                            });
                        }
                    }

                    _viewModel.MarkerList = final;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowIfError();
        }

        [ExcludeFromCodeCoverage]
        private void ShowIfError()
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
