using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(PenerimaanLainnyaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                { "IdDaftarPenerimaanKas", _viewModel.SelectedData.IdDaftarPenerimaanKas! }
            };

            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/penerimaan-lainnya", param));
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

            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
