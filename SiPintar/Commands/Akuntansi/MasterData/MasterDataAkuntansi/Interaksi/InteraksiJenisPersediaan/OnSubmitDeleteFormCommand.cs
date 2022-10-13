using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(InteraksiJenisPersediaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "AkuntansiRootDialog", "Mohon tunggu", "sedang memproses tindakan...");

            //Start - Submit Delete
            var param = new Dictionary<string, dynamic>
            {
                { "IdPdam", _viewModel.SelectedData.IdPdam! },
                { "IdInPersediaan", _viewModel.SelectedData.IdInPersediaan! }
            };

            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-inpersediaan", param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "akuntansi");
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "akuntansi");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message!, "akuntansi");
            }

            //Start - Submit Delete
            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            //End - Get Filter Data
            await (_viewModel.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);

            await Task.FromResult(_isTest);
        }
    }
}
