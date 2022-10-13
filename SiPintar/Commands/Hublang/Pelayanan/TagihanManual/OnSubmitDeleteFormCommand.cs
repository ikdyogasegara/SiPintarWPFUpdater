using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(TagihanManualViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic>
            {
                { "IdNonAir", _viewModel.SelectedData.IdNonAir }
            };

            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air-hapus", body));

            if (!response.IsError)
            {
                DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);
                var result = response.Data;
                DialogHelper.ShowSnackbar(_isTest, result.Status ? "success" : "danger", result.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }
    }
}
