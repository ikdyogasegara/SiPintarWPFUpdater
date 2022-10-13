using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(DiklatPelatihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");


            var body = new Dictionary<string, dynamic>
            {
                { "NoSertifikat", _viewModel.FormNoSertifikat },
                { "Uraian", _viewModel.FormUraian },
                { "Tahun", _viewModel.FormTglAwal.Value.Year },
                { "TglAwal", _viewModel.FormTglAwal },
                { "TglAkhir", _viewModel.FormTglAkhir },
                { "Tempat", _viewModel.FormTempat },
                { "Penyelenggara", _viewModel.FormPenyelenggara },
                { "Detail", _viewModel.FormDetailList },
            };

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/diklat", body));
            else
            {
                body.Add("IdDiklat", _viewModel.SelectedData.IdDiklat);
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/diklat", null, body));
            }

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
