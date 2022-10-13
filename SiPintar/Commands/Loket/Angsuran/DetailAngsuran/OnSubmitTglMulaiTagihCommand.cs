using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;


namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnSubmitTglMulaiTagihCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitTglMulaiTagihCommand(DetailAngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");

                dynamic tglMulaiTagihList = null;
                RestApiResponse Response = null;
                if (_viewModel.Parent.ParentCurrentSection == "Tunggakan")
                {
                    tglMulaiTagihList = new List<ParamRekeningAngsuranRekeningAirListTglMulaiTagihDto>() { new ParamRekeningAngsuranRekeningAirListTglMulaiTagihDto()
                    {
                        Id = _viewModel.DetailAngsuran.Id,
                        TglMulaiTagih = _viewModel.SelectedDate
                    }};

                    var param = new Dictionary<string, dynamic>
                    {
                        { "Detail", tglMulaiTagihList},
                    };

                    Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-air-set-tglmulaitagih", null, param));

                }
                else
                {
                    tglMulaiTagihList = new List<ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto>() { new ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto()
                    {
                        Id = _viewModel.DetailAngsuran.Id,
                        TglMulaiTagih = _viewModel.SelectedDate
                    }};

                    var param = new Dictionary<string, dynamic>
                    {
                        { "Detail", tglMulaiTagihList},
                    };

                    Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air-set-tglmulaitagih", null, param));

                }

                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "loket");
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");
                }

            }
            catch (Exception) { throw; }

            _ = ((AsyncCommandBase)_viewModel.OnLoadCommandAngsuran).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
