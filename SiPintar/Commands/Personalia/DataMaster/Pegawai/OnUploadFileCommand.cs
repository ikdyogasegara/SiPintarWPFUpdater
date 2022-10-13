using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnUploadFileCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnUploadFileCommand(PegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FotoPegawaiUri == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPegawai", _viewModel.FormData.IdPegawai }
            };

            if (_viewModel.FotoPegawaiUri != null && !_viewModel.FotoPegawaiUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_Foto", _viewModel.FotoPegawaiUri.OriginalString);
            if (_viewModel.FotoKtpUri != null && !_viewModel.FotoKtpUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_FotoKtp", _viewModel.FotoKtpUri.OriginalString);
            if (_viewModel.FotoKkUri != null && !_viewModel.FotoKkUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_FotoKk", _viewModel.FotoKkUri.OriginalString);

            if (_isTest)
                body.Add("TestId", _viewModel.FormData.IdPegawai);

            var Response = await Task.Run(() => _restApi.UploadAsync($"/api/{ApiVersion}/master-pegawai-upload-foto", body));
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

            if (ErrorMessage != null)
                DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "personalia");

            if (SuccessMessage != null)
                Debug.WriteLine(SuccessMessage);

            await Task.FromResult(_isTest);
        }
    }
}
