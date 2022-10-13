using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnUploadFotoCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUploadFotoCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FotoSkUri == null)
                return;

            object dg = null;
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic>
            {
                { "IdMutasi", _viewModel.SelectedData.IdMutasi }
            };

            if (_viewModel.FotoSkUri != null && !_viewModel.FotoSkUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_FotoSk", _viewModel.FotoSkUri.OriginalString);

            if (_isTest)
                body.Add("TestId", _viewModel.SelectedData.IdMutasi);

            var Response = await Task.Run(() => _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/mutasi-status-pegawai-tetap-upload-foto", body));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.FromResult(_isTest);
        }
    }
}
