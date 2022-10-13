using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    public class OnSubmitResetPasswordCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitResetPasswordCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            DialogHelper.ShowLoading(_isTest, "BillingRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdPetugasBaca", _viewModel.FormData.IdPetugasBaca },
                { "KodePetugasBaca", _viewModel.FormData.KodePetugasBaca },
                { "PetugasBaca", _viewModel.FormData.PetugasBaca },
                { "JenisPembaca", _viewModel.FormData.JenisPembaca },
                { "NamaUser", _viewModel.FormData.NamaUser },
                { "Password", _viewModel.PasswordForm },
                { "Alamat", _viewModel.FormData.Alamat },
                { "TglLahir", _viewModel.FormData.TglLahir },
                { "TglMulaiKerja", _viewModel.FormData.TglMulaiKerja },
                { "Status", _viewModel.FormData.Status }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-petugas-baca", null, body));
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

            if (App.OpenedWindow.ContainsKey("billing"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BillingRootDialog",
                    App.OpenedWindow["billing"], true, _viewModel.OnLoadCommand, true);

            _viewModel.IsLoadingForm = false;
        }
    }
}
