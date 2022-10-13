using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;


namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(KeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var dataAdd = _viewModel.SelectedKeluargaListForm.Where(k => k.IdKeluarga == null);
            var dataEdit = _viewModel.SelectedKeluargaListForm.Where(k =>
            _viewModel.SelectedKeluargaList.Any(s => k.IdKeluarga == s.IdKeluarga) &&
            JsonSerializer.Serialize(k) != JsonSerializer.Serialize(_viewModel.SelectedKeluargaList.FirstOrDefault(s => k.IdKeluarga == s.IdKeluarga)));
            var dataDelete = _viewModel.SelectedKeluargaList.Where(k => !_viewModel.SelectedKeluargaListForm.Any(s => k.IdKeluarga == s.IdKeluarga));

            if (dataAdd.Any())
            {
                foreach (var item in dataAdd)
                {
                    var body = new Dictionary<string, dynamic>
                    {
                        { "IdPegawai", _viewModel.SelectedData.IdPegawai },
                        { "NamaKeluarga", item.NamaKeluarga },
                        { "Status", item.Status },
                        { "IdPekerjaan", item.IdPekerjaan },
                        { "IdJenisKelamin", item.IdJenisKelamin },
                        { "NoPenduduk", item.NoPenduduk },
                        { "TglLahir", item.TglLahir },
                        { "NoBpjsKes", item.NoBpjsKes },
                        { "FlagKawin", item.FlagKawin },
                        { "FlagTanggung", item.FlagTanggung },
                    };

                    RestApiResponse Response = null;
                    Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-keluarga", body));

                    if (!Response.IsError)
                    {
                        var Result = Response.Data;
                        DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
                }
            }

            if (dataEdit.Any())
            {
                foreach (var item in dataEdit)
                {
                    var body = new Dictionary<string, dynamic>
                    {
                        { "IdKeluarga", item.IdKeluarga },
                        { "IdPegawai", item.IdPegawai },
                        { "NamaKeluarga", item.NamaKeluarga },
                        { "Status", item.Status },
                        { "IdPekerjaan", item.IdPekerjaan },
                        { "IdJenisKelamin", item.IdJenisKelamin },
                        { "NoPenduduk", item.NoPenduduk },
                        { "TglLahir", item.TglLahir },
                        { "NoBpjsKes", item.NoBpjsKes },
                        { "FlagKawin", item.FlagKawin },
                        { "FlagTanggung", item.FlagTanggung },
                    };

                    RestApiResponse Response = null;
                    Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-keluarga", null, body));

                    if (!Response.IsError)
                    {
                        var Result = Response.Data;
                        DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
                }
            }

            if (dataDelete.Any())
            {
                foreach (var item in dataDelete)
                {
                    var param = new Dictionary<string, dynamic> { { "IdKeluarga", item.IdKeluarga } };

                    RestApiResponse Response = null;
                    Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-keluarga", param));

                    if (!Response.IsError)
                    {
                        var Result = Response.Data;
                        DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
                }
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }
    }
}
