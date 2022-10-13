using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PetugasBaca
{
    public class UploadFileCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public UploadFileCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FotoPetugasURI == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPetugasBaca", _viewModel.FormData.IdPetugasBaca }
            };

            if (_viewModel.FotoPetugasURI != null && !_viewModel.FotoPetugasURI.OriginalString.ToLower().Contains("http"))
                body.Add("file_FotoPetugasBaca", _viewModel.FotoPetugasURI.OriginalString);

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.UploadAsync($"/api/{ApiVersion}/master-petugas-baca-upload-foto", body));
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

            ShowSnackbar(ErrorMessage);

            if (SuccessMessage != null)
                Debug.WriteLine(SuccessMessage);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
