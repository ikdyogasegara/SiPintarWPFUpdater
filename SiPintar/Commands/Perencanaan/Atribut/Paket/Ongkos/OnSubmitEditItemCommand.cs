using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    public class OnSubmitEditItemCommand : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditItemCommand(PaketOngkosViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var DataToSend = new List<MasterPaketOngkosDetailDto>();
            var OngkosData = new List<MasterPaketOngkosDetailDto>(_viewModel.SelectedPaket.Detail);

            foreach (var item in OngkosData)
            {
                if (item.IdOngkos == _viewModel.SelectedRincian.IdOngkos)
                    item.Qty = _viewModel.KuantitasForm;

                DataToSend.Add(item);
            }

            var body = new Dictionary<string, dynamic>
            {
                { "IdPaketOngkos", _viewModel.SelectedPaket.IdPaketOngkos },
                { "Detail", DataToSend }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-paket-ongkos", null, body));
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
