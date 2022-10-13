using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;


namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PengecualianTunjanganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                {"IdPegawai", _viewModel.SelectedDataPegawai.IdPegawai },
                {"FlagRutin", _viewModel.FormFlagRutin },
                {"KodePeriode", _viewModel.FormKodePeriode },
                {"Keterangan", _viewModel.FormKeterangan },
                {"Detail", _viewModel.FormPengecualian == "SEMUA" ? _viewModel.JenisTunjanganListForm.ToList() : _viewModel.JenisTunjanganListForm.Where(j=>j.IsChecked).ToList() },
            };

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/pengecualian-tunjangan", body));
            else
            {
                body.Add("IdPengecualianTunjangan", _viewModel.SelectedData.IdPengecualianTunjangan);
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/pengecualian-tunjangan", null, body));
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
