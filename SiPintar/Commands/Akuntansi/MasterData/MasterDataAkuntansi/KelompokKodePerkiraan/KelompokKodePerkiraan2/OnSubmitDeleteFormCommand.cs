using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(KelompokKodePerkiraan2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);

            //Start - Submit Delete
            var param = new Dictionary<string, dynamic>();
            param.Add("IdPdam", _viewModel.SelectedData?.IdPdam);
            param.Add("IdPerkiraan2", _viewModel.SelectedData?.IdPerkiraan2);
            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-perkiraan2", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "akuntansi");
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "akuntansi");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "akuntansi");
            }

            //Start - Submit Delete
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            //End - Get Filter Data
            await ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null);
        }
    }
}
