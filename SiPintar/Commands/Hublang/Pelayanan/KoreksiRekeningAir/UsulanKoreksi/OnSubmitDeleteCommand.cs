using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(UsulanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdPermohonan", _viewModel.SelectedData.IdPermohonan },
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
            };

            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/permohonan-koreksi-rekening-air", body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    successMessage = response.Data.Ui_msg;
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
                errorMessage = response.Error.Message;

            if (App.OpenedWindow.ContainsKey("hublang"))
                DialogHelper.ShowSuccessErrorDialog(errorMessage, successMessage, _isTest, "HublangRootDialog", App.OpenedWindow["hublang"], true, _viewModel.OnRefreshCommand);

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }
    }
}
