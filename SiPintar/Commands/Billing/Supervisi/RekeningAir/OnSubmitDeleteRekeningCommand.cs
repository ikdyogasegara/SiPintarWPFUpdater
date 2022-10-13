using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSubmitDeleteRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteRekeningCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "BillingRootDialog",
                "Mohon tunggu",
                "sedang memproses tindakan...");

            var entityId = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode },
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir }
            };

            var response = await _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-hapus", entityId);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                    var idxHapus = _viewModel.RekeningAirList.IndexOf(_viewModel.SelectedData);
                    if (idxHapus > -1)
                    {
                        _viewModel.RekeningAirList.RemoveAt(idxHapus);
                        idxHapus--;
                    }

                    if (idxHapus >= 0)
                    {
                        _viewModel.SelectedData = _viewModel.RekeningAirList[idxHapus];
                        _viewModel.InvokeUpdateDataGrid();
                    }
                    else
                    {
                        await ((AsyncCommandBase)_viewModel.OnFilterCommand).ExecuteAsync(null);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);
        }
    }
}
