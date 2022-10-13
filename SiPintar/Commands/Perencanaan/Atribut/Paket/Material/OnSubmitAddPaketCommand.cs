using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnSubmitAddPaketCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitAddPaketCommand(PaketMaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            var detail = new List<MasterPaketMaterialDetailDto>();
            var body = new Dictionary<string, dynamic>
            {
                { "KodePaketMaterial", _viewModel.KodePaketForm },
                { "NamaPaketMaterial", _viewModel.NamaPaketForm },
                { "Detail", detail }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-paket-material", body));
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
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest,
                    "PerencanaanRootDialog", App.OpenedWindow["perencanaan"], true, _viewModel.OnLoadCommand);

            _viewModel.IsLoadingForm = false;
        }
    }
}
