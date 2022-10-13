using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.Denah
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DenahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DenahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            RestApiResponse Response = null;
            Dictionary<string, dynamic> param = null;
            int? IdPelangganAir = null;
            int? IdPelangganLimbah = null;
            int? IdPelangganLltt = null;

            Dictionary<string, dynamic> paramCekPelanggan = null;
            ObservableCollection<MasterPelangganLimbahDto> masterPelangganLimbahDto = null;
            ObservableCollection<MasterPelangganLlttDto> masterPelangganLlttDto = null;

            if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganAirDto;
                IdPelangganAir = data.IdPelangganAir;
            }
            else if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLimbahDto;
                IdPelangganAir = data.IdPelangganAir;
                IdPelangganLimbah = data.IdPelangganLimbah;
            }
            else
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLlttDto;
                IdPelangganAir = data.IdPelangganAir;
                IdPelangganLltt = data.IdPelangganLltt;
            }

            if (IdPelangganAir != null)
            {
                if (IdPelangganLimbah == null)
                {
                    paramCekPelanggan = new Dictionary<string, dynamic>
                    {
                        { "Limit" , 1},
                        { "IdPelangganAir", IdPelangganAir ?? 0}
                    };

                    Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-limbah", paramCekPelanggan));

                    if (!Response.IsError)
                    {
                        var Result = Response.Data;

                        if (Result.Status && Result.Data != null)
                        {
                            masterPelangganLimbahDto = Result.Data.ToObject<ObservableCollection<MasterPelangganLimbahDto>>();
                            if (masterPelangganLimbahDto.Count > 0)
                            {
                                IdPelangganLimbah = masterPelangganLimbahDto.FirstOrDefault().IdPelangganLimbah;
                            }
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
                    }
                }

                if (IdPelangganLltt == null)
                {
                    paramCekPelanggan = new Dictionary<string, dynamic>
                    {
                        { "Limit" , 1},
                        { "IdPelangganAir", IdPelangganAir ?? 0}
                    };

                    Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-lltt", paramCekPelanggan));

                    if (!Response.IsError)
                    {
                        var Result = Response.Data;

                        if (Result.Status && Result.Data != null)
                        {
                            masterPelangganLlttDto = Result.Data.ToObject<ObservableCollection<MasterPelangganLlttDto>>();
                            if (masterPelangganLlttDto.Count > 0)
                            {
                                IdPelangganLltt = masterPelangganLlttDto.FirstOrDefault().IdPelangganLltt;
                            }
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
                    }
                }
            }

            param = new Dictionary<string, dynamic>
                {
                    { "Limit" , 100000000 },
                    { "IdPelangganAir", IdPelangganAir ?? 0},
                    { "IdPelangganLimbah", IdPelangganLimbah ?? 0},
                    { "IdPelangganLltt", IdPelangganLltt ?? 0},
                    { "AdaDenah", true}
                };

            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/histori-permohonan-global", param));
            _viewModel.DenahList = new ObservableCollection<SummaryHistoriPermohonanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DenahList = Result.Data.ToObject<ObservableCollection<SummaryHistoriPermohonanDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
            }


            _viewModel.IsEmpty = _viewModel.DenahList.Count == 0;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
