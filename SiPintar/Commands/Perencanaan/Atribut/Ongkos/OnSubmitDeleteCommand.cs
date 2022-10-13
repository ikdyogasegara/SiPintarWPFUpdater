using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Ongkos
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly OngkosViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(OngkosViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var entityId = new Dictionary<string, dynamic>
            {
                { "IdOngkos", _viewModel.SelectedData.IdOngkos }
            };

            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/master-ongkos", entityId));
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

            _viewModel.IsLoadingForm = false;

            if (App.OpenedWindow.ContainsKey("perencanaan"))
                DialogHelper.SnackbarPerencanaanHandler(ErrorMessage, SuccessMessage, "error", "success", _isTest, "AtributOngkosDialog", App.OpenedWindow["perencanaan"]);

            _viewModel.OnFilterCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
