using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.Pensiun
{
    public class OnSubmitVerifikasiFormCommand : AsyncCommandBase
    {
        private readonly PensiunViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitVerifikasiFormCommand(PensiunViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic>
            {
                { "IdPegawai", _viewModel.SelectedData.IdPegawai },
                { "IdJenisPensiun", _viewModel.FormJenisPensiun },
                { "NoSkPensiun", $"{_viewModel.FormSk1}/{_viewModel.FormSk2}/{_viewModel.FormSk3}/{_viewModel.FormSk4}/{_viewModel.FormSk5}" },
                { "TglSkPensiun", _viewModel.FormTgl },
            };

            RestApiResponse Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai-verifikasi-pensiun", body));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            await Task.FromResult(_isTest);
        }
    }
}
