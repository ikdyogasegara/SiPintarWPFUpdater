using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdPengembalian", _viewModel.SelectedData.IdPengembalian}
            };


            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/rekening-air-pengembalian", body));
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

            if (App.OpenedWindow.ContainsKey("hublang"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "HublangRootDialog", App.OpenedWindow["hublang"], true, _viewModel.OnRefreshCommand);

        }
    }
}
