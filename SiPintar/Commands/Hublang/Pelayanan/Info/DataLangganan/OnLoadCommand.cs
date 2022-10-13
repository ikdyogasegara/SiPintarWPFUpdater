using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.DataLangganan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DataLanggananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DataLanggananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            TemporariDataHelper.IdPelangganAir = null;
            TemporariDataHelper.IdPelangganLimbah = null;
            TemporariDataHelper.IdPelangganLltt = null;

            RestApiResponse ResponsePelangganAir = null;
            RestApiResponse ResponsePelangganLimbah = null;
            RestApiResponse ResponsePelangganLltt = null;



            _viewModel.DetailPelangganAir = null;
            _viewModel.DetailPelangganLimbah = null;
            _viewModel.DetailPelangganLltt = null;

            if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganAirDto;
                int? IdPelangganAir = data.IdPelangganAir;

                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 1 },
                    { "CurrentPage" , 1},
                    { "IdPelangganAir", IdPelangganAir}
                };

                ResponsePelangganAir = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param));
                ResponsePelangganLimbah = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-limbah", param));
                ResponsePelangganLltt = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-lltt", param));

            }
            else if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLimbahDto;
                int? IdPelangganLimbah = data.IdPelangganLimbah;
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 1 },
                    { "CurrentPage" , 1},
                    { "IdPelangganLimbah", IdPelangganLimbah}
                };

                int? IdPelangganAir = data.IdPelangganAir ?? 0;
                var paramByIdPelangganAir = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 1 },
                    { "CurrentPage" , 1},
                    { "IdPelangganAir", IdPelangganAir}
                };

                ResponsePelangganLimbah = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-limbah", param));
                ResponsePelangganAir = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", paramByIdPelangganAir));
                ResponsePelangganLltt = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-lltt", paramByIdPelangganAir));
            }
            else
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLlttDto;
                int? IdPelangganLltt = data.IdPelangganLltt;
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 1 },
                    { "CurrentPage" , 1},
                    { "IdPelangganLltt", IdPelangganLltt}
                };


                int? IdPelangganAir = data.IdPelangganAir ?? 0;
                var paramByIdPelangganAir = new Dictionary<string, dynamic>
                    {
                        { "PageSize" , 1 },
                        { "CurrentPage" , 1},
                        { "IdPelangganAir", IdPelangganAir}
                    };

                ResponsePelangganLltt = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-lltt", param);
                ResponsePelangganAir = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", paramByIdPelangganAir);
                ResponsePelangganLimbah = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-limbah", paramByIdPelangganAir);
            }

            if (!ResponsePelangganAir.IsError && !ResponsePelangganLimbah.IsError && !ResponsePelangganLltt.IsError)
            {
                var ResultPelangganAir = ResponsePelangganAir.Data;
                var ResultPelangganLimbah = ResponsePelangganLimbah.Data;
                var ResultPelangganLltt = ResponsePelangganLltt.Data;

                if (ResultPelangganAir.Status && ResultPelangganAir.Data != null)
                {
                    var temp = ResultPelangganAir.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();
                    _viewModel.DetailPelangganAir = temp.Count > 0 ? temp[0] : null;
                    TemporariDataHelper.IdPelangganAir = temp.Count > 0 ? temp[0].IdPelangganAir : null;
                }

                if (ResultPelangganLimbah.Status && ResultPelangganLimbah.Data != null)
                {
                    var temp = ResultPelangganLimbah.Data.ToObject<ObservableCollection<MasterPelangganLimbahDto>>();
                    _viewModel.DetailPelangganLimbah = temp.Count > 0 ? temp[0] : null;
                    TemporariDataHelper.IdPelangganLimbah = temp.Count > 0 ? temp[0].IdPelangganLimbah : null;
                }

                if (ResultPelangganLltt.Status && ResultPelangganLltt.Data != null)
                {
                    var temp = ResultPelangganLltt.Data.ToObject<ObservableCollection<MasterPelangganLlttDto>>();
                    _viewModel.DetailPelangganLltt = temp.Count > 0 ? temp[0] : null;
                    TemporariDataHelper.IdPelangganLltt = temp.Count > 0 ? temp[0].IdPelangganLltt : null;
                }
            }
            else
            {
                string errorMsg = "Terjadi Kesalahan !";

                if (ResponsePelangganAir.Error.Message != null)
                {
                    errorMsg = ResponsePelangganAir.Error.Message;
                }

                if (ResponsePelangganLimbah.Error.Message != null)
                {
                    errorMsg = ResponsePelangganLimbah.Error.Message;
                }

                if (ResponsePelangganLltt.Error.Message != null)
                {
                    errorMsg = ResponsePelangganLltt.Error.Message;
                }

                DialogHelper.ShowSnackbar(_isTest, "danger", errorMsg);
            }

            _viewModel.IsEmpty = false;
            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
