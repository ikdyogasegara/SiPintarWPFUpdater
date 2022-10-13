using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;


namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(TunjanganBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                {"IdKodeGaji", _viewModel.FilterKodeGaji.IdKodeGaji },
                {"KodePeriode", _viewModel.FilterPeriode.KodePeriode },
                {"IdJenisTunjangan", _viewModel.FormJenisTunjangan },
                {"Beban", _viewModel.FormBeban },
                {"Keterangan", _viewModel.FormKeterangan },
                {"FlagAbsensi", _viewModel.FormFlagAbsensi },
                {"FlagPersen", _viewModel.FormFlagPersen },
                {"FlagPotongPph", _viewModel.FormFlagPotongPph },
                {"FlagSemuaPegawai", _viewModel.FormFlagSemuaPegawai },
            };

            if (_viewModel.FormFlagAbsensi == true)
                body.Add("IdUpahKehadiran", _viewModel.FormUpahKehadiran);
            else
                body.Add("Nominal", _viewModel.FormNominal);

            if (_viewModel.FormFlagPersen == true)
            {
                body.Add("PersenDari", _viewModel.FormPersenDari);
                if (_viewModel.FormIsNominalMinChecked == true)
                    body.Add("NominalMin", _viewModel.FormNominalMin);
                if (_viewModel.FormIsNominalMaxChecked == true)
                    body.Add("NominalMax", _viewModel.FormNominalMax);
            }

            if (_viewModel.FormFlagPotongPph == true)
            {
                body.Add("FlagPersenPph", _viewModel.FormFlagPersenPph);
                body.Add("NominalPph", _viewModel.FormNominalPph);
            }

            if (_viewModel.FormFlagSemuaPegawai != true)
            {
                if (_viewModel.FormIsAgamaChecked)
                    body.Add("IdAgama", _viewModel.FormAgama);
                if (_viewModel.FormIsAreaKerjaChecked)
                    body.Add("IdAreaKerja", _viewModel.FormAreaKerja);
                if (_viewModel.FormIsDepartemenChecked)
                    body.Add("IdDepartemen", _viewModel.FormDepartemen);
                if (_viewModel.FormIsDivisiChecked)
                    body.Add("IdDivisi", _viewModel.FormDivisi);
                if (_viewModel.FormIsGolonganPegawaiChecked)
                    body.Add("IdGolonganPegawai", _viewModel.FormGolonganPegawai);
                if (_viewModel.FormIsJabatanChecked)
                    body.Add("IdJabatan", _viewModel.FormJabatan);
                if (_viewModel.FormIsJenisKelaminChecked)
                    body.Add("IdJenisKelamin", _viewModel.FormJenisKelamin);
                if (_viewModel.FormIsPendidikanChecked)
                    body.Add("IdPendidikan", _viewModel.FormPendidikan);
                if (_viewModel.FormIsTipeKeluargaChecked)
                    body.Add("IdTipeKeluarga", _viewModel.FormTipeKeluarga);
                if (_viewModel.FormIsStatusPegawaiChecked)
                    body.Add("IdStatusPegawai", _viewModel.FormStatusPegawai);
                if (_viewModel.FormIsPegawaiChecked)
                    body.Add("IdPegawai", _viewModel.FormPegawai);
                if (_viewModel.FormIsMkgChecked)
                    body.Add("Mkg", _viewModel.FormMkg);
            }

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-set-tunjangan-bulanan", body));
            else
            {
                body.Add("IdTunjanganBulanan", _viewModel.SelectedData.IdTunjanganBulanan);
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-set-tunjangan-bulanan", null, body));
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
