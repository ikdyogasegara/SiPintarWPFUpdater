using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnSubmitResetPasswordCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitResetPasswordCommand(DaftarPenggunaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "MainRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdUser", _viewModel.FormData.IdUser! },
                { "Nama", _viewModel.FormData.Nama },
                { "NamaUser", _viewModel.FormData.NamaUser },
                { "IdRole", _viewModel.FormData.IdRole },
                { "Password", _viewModel.FormData.Password }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-user", null, body));
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

            if (App.OpenedWindow.ContainsKey("main"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "MainRootDialog",
                    App.OpenedWindow["main"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;
        }
    }
}
