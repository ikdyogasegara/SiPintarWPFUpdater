using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            param.Add("IdPdam", _viewModel.SelectedData.IdPdam);
            param.Add("IdPelangganAir", _viewModel.SelectedData.IdPelangganAir);
            param.Add("Tahun", _viewModel.Parent.TahunFilter);
            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "distribusi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "distribusi");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "distribusi");
            }

            //Start - Submit Delete
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);

            //End - Get Filter Data
            var filter = _viewModel.Parent.Filter;
            await ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(filter);
            await Task.FromResult(_isTest);
        }
    }
}
