using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.AreaWilayah
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly AreaWilayahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(AreaWilayahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "BillingRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "BillingRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.IsLoadingForm = true;

            var entityId = new Dictionary<string, dynamic>();

            string url = null;
            if (_viewModel.Section == "wilayah")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-wilayah";

                entityId.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);
            }
            else if (_viewModel.Section == "area")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-area";

                entityId.Add("IdArea", _viewModel.SelectedArea.IdArea);
            }
            else if (_viewModel.Section == "rayon")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-rayon";

                entityId.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);
            }

            var response = await Task.Run(() => _restApi.DeleteAsync(url, entityId));

            DialogHelper.CloseDialog(_isTest, false, "BillingRootDialog", dg);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                    Reload();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void Reload()
        {
            if (_viewModel.Section == "wilayah")
            {
                _viewModel.SelectedWilayah = null;
                _viewModel.WilayahList.Clear();
            }
            else if (_viewModel.Section == "area")
            {
                _viewModel.SelectedArea = null;
                _viewModel.AreaList.Clear();
            }
            else if (_viewModel.Section == "rayon")
            {
                _viewModel.SelectedRayon = null;
                _viewModel.RayonList.Clear();
            }

            _viewModel.OnLoadCommand.Execute(_viewModel.Section);
        }
    }
}
