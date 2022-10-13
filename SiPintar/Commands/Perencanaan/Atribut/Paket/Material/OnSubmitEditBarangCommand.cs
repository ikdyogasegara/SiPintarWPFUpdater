using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnSubmitEditBarangCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditBarangCommand(PaketMaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var DataToSend = new List<MasterPaketMaterialDetailDto>();
            var MaterialData = new List<MasterPaketMaterialDetailDto>(_viewModel.SelectedPaket.Detail);

            foreach (var item in MaterialData)
            {
                if (item.IdMaterial == _viewModel.SelectedRincian.IdMaterial)
                    item.Qty = _viewModel.KuantitasForm;

                DataToSend.Add(item);
            }

            var body = new Dictionary<string, dynamic>
            {
                { "IdPaketMaterial", _viewModel.SelectedPaket.IdPaketMaterial },
                { "Detail", DataToSend }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-paket-material", null, body));
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
