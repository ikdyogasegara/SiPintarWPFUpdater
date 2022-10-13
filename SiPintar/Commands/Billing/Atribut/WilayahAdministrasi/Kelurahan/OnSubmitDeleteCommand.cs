using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Kelurahan
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly KelurahanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(KelurahanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            if (_viewModel.Section == "cabang")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-cabang";

                entityId.Add("IdCabang", _viewModel.SelectedCabang.IdCabang);
            }
            else if (_viewModel.Section == "kecamatan")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-kecamatan";

                entityId.Add("IdKecamatan", _viewModel.SelectedKecamatan.IdKecamatan);
            }
            else if (_viewModel.Section == "kelurahan")
            {
                url = $"/api/{_restApi.GetApiVersion()}/master-kelurahan";

                entityId.Add("IdKelurahan", _viewModel.SelectedKelurahan.IdKelurahan);
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
            if (_viewModel.Section == "cabang")
            {
                _viewModel.SelectedCabang = null;
                _viewModel.CabangList.Clear();
            }
            else if (_viewModel.Section == "kecamatan")
            {
                _viewModel.SelectedKecamatan = null;
                _viewModel.KecamatanList.Clear();
            }
            else if (_viewModel.Section == "kelurahan")
            {
                _viewModel.SelectedKelurahan = null;
                _viewModel.KelurahanList.Clear();
            }

            _viewModel.OnLoadCommand.Execute(_viewModel.Section);
        }
    }
}
