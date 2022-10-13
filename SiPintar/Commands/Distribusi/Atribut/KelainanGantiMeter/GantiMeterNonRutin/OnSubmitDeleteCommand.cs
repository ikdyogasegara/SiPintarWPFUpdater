using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(GantiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "DistribusiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);

            //Start - Submit Delete
            var param = new Dictionary<string, dynamic>();
            param.Add("IdPdam", _viewModel.SelectedData?.IdPdam);
            param.Add("IdJenisGantiMeter", _viewModel.SelectedData?.IdJenisGantiMeter);
            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-ganti-meter", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "distribusi");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "distribusi");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "distribusi");
            }

            //Start - Submit Delete
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);

            //End - Get Filter Data
            await ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
