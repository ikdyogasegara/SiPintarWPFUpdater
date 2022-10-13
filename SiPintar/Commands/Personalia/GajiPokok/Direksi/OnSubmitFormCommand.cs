using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;


namespace SiPintar.Commands.Personalia.GajiPokok.Direksi
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly DireksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(DireksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                {"IdPegawai", _viewModel.SelectedDataPegawaiForm.IdPegawai },
                {"FlagPersen", _viewModel.FormFlagPersen },
            };

            if (_viewModel.FormFlagPersen == false)
            {
                body.Add("Jumlah", _viewModel.FormNominalNonPersen);
                body.Add("Persentase", 0);
            }
            else
            {
                body.Add("Jumlah", 0);
                body.Add("Persentase", _viewModel.FormNominalPersen);

                body.Add("Berdasarkan", _viewModel.FormBerdasarkan);
                if (_viewModel.FormBerdasarkan == "GAJI PEGAWAI")
                    body.Add("IdPegawaiRef", _viewModel.SelectedDataPegawaiRefForm.IdPegawai);

                body.Add("PersenDari", _viewModel.FormPersenDari);
                if (_viewModel.FormPersenDari.Contains("GAJI POKOK + TUNJANGAN"))
                {
                    _viewModel.FormIdTunjangan = _viewModel.TunjanganList.FirstOrDefault(t => t.IsChecked)?.IdJenisTunjangan;
                    body.Add("IdJenisTunjangan", _viewModel.FormIdTunjangan);
                }
                if (_viewModel.FormPersenDari == "GAJI POKOK + TUNJANGAN - POTONGAN")
                {
                    _viewModel.FormIdPotongan = _viewModel.PotonganList.FirstOrDefault(t => t.IsChecked)?.IdJenisPotongan;
                    body.Add("IdJenisPotongan", _viewModel.FormIdPotongan);
                }
            }

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-gaji-pegawai-direksi", body));
            else
            {
                body.Add("IdGajiDireksi", _viewModel.SelectedData.IdGajiDireksi);
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-gaji-pegawai-direksi", null, body));
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
