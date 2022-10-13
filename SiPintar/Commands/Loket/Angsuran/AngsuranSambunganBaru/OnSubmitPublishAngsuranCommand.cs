using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    public class OnSubmitPublishAngsuranCommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitPublishAngsuranCommand(AngsuranSambunganBaruViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(
            _isTest,
            true,
            "LoketRootDialog",
            "Mohon tunggu",
            "sedang memproses tindakan...");

            var param = new Dictionary<string, dynamic>
                {
                    { "IdAngsuran", _viewModel.SelectedData.IdAngsuran },
                };
            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air-publish", null, param));

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

            await ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null);
            DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog", dg);
            _ = await Task.FromResult(_isTest);
        }
    }
}
