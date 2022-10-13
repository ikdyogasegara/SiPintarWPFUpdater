using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;


namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnVerifyCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnVerifyCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.Verifikasi == true)
                    return;

                object dg = null;
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

                var body = new Dictionary<string, dynamic> { { "IdMutasi", _viewModel.SelectedData.IdMutasi } };

                RestApiResponse Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/mutasi-status-pegawai-tetap-verifikasi", null, body));

                DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

                await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Verifikasi SK Pegawai Tetap",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
