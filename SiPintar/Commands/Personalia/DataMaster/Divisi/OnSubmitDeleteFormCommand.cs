using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Divisi
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly DivisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(DivisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic> { { "IdDivisi", _viewModel.SelectedData.IdDivisi } };

            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-divisi", body));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            await Task.FromResult(_isTest);
        }
    }
}
