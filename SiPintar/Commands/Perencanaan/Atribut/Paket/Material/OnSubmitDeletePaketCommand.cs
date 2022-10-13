using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnSubmitDeletePaketCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeletePaketCommand(PaketMaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "PerencanaanRootDialog");

            var param = new Dictionary<string, dynamic>
            {
                { "IdPaketMaterial", _viewModel.SelectedPaket.IdPaketMaterial }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/master-paket-material", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("perencanaan"))
            {
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "PerencanaanRootDialog", App.OpenedWindow["perencanaan"],
                    true, _viewModel.OnLoadCommand);
            }

            if (SuccessMessage != null)
                _viewModel.SelectedPaket = null;

            _viewModel.IsLoadingForm = false;
        }
    }
}
