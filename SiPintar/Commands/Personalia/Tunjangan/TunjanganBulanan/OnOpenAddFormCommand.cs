using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.TunjanganBulanan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TunjanganBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormCurrentPage = 1;

            await GetJenisTunjanganAsync();
            await GetUpahKehadiranAsync();
            await GetAgamaAsync();
            await GetAreaKerjaAsync();
            await GetDepartemenAsync();
            await GetDivisiAsync();
            await GetGolonganAsync();
            await GetJabatanAsync();
            await GetJenisKelaminAsync();
            await GetPendidikanAsync();
            await GetTipeKeluargaAsync();
            await GetStatusPegawaiAsync();

            _viewModel.FormJenisTunjangan = null;
            _viewModel.FormFlagAbsensi = true;
            _viewModel.FormUpahKehadiran = null;
            _viewModel.FormNominal = 0;

            _viewModel.FormBeban = "Pegawai";
            _viewModel.FormFlagPersen = false;
            _viewModel.FormPersenDari = null;
            _viewModel.FormNominalMin = 0;
            _viewModel.FormNominalMax = 0;

            _viewModel.FormKeterangan = null;
            _viewModel.FormFlagPotongPph = false;
            _viewModel.FormFlagPersenPph = null;
            _viewModel.FormNominalPph = 0;

            _viewModel.FormFlagSemuaPegawai = true;

            _viewModel.FormIsAgamaChecked = false;
            _viewModel.FormIsDivisiChecked = false;
            _viewModel.FormIsJabatanChecked = false;
            _viewModel.FormIsPendidikanChecked = false;

            _viewModel.FormIsAreaKerjaChecked = false;
            _viewModel.FormIsGolonganPegawaiChecked = false;
            _viewModel.FormIsJenisKelaminChecked = false;
            _viewModel.FormIsTipeKeluargaChecked = false;

            _viewModel.FormIsDepartemenChecked = false;
            _viewModel.FormIsMkgChecked = false;
            _viewModel.FormIsPegawaiChecked = false;
            _viewModel.FormIsStatusPegawaiChecked = false;

            _viewModel.FormAgama = null;
            _viewModel.FormDivisi = null;
            _viewModel.FormJabatan = null;
            _viewModel.FormPendidikan = null;
            _viewModel.FormAreaKerja = null;
            _viewModel.FormGolonganPegawai = null;
            _viewModel.FormJenisKelamin = null;
            _viewModel.FormTipeKeluarga = null;
            _viewModel.FormDepartemen = null;
            _viewModel.FormMkg = 0;
            _viewModel.FormPegawai = null;
            _viewModel.FormStatusPegawai = null;

            _viewModel.SelectedDataPegawai = new MasterPegawaiDto();

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetJenisTunjanganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-tunjangan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisTunjanganListForm = Result.Data.ToObject<ObservableCollection<MasterJenisTunjanganDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetUpahKehadiranAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-upah-kehadiran");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.UpahKehadiranListForm = Result.Data.ToObject<ObservableCollection<MasterUpahKehadiranDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetAgamaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-agama");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AgamaListForm = Result.Data.ToObject<ObservableCollection<MasterAgamaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetAreaKerjaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-area-kerja");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AreaKerjaListForm = Result.Data.ToObject<ObservableCollection<MasterAreaKerjaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetDepartemenAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-departemen");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DepartemenListForm = Result.Data.ToObject<ObservableCollection<MasterDepartemenDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetDivisiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-divisi");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DivisiListForm = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetGolonganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-golongan-pegawai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganPegawaiListForm = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetJabatanAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jabatan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JabatanListForm = Result.Data.ToObject<ObservableCollection<MasterJabatanDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetJenisKelaminAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-kelamin");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisKelaminListForm = Result.Data.ToObject<ObservableCollection<MasterJenisKelaminDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetPendidikanAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pendidikan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PendidikanListForm = Result.Data.ToObject<ObservableCollection<MasterPendidikanDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetTipeKeluargaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tipe-keluarga");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TipeKeluargaListForm = Result.Data.ToObject<ObservableCollection<MasterTipeKeluargaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetStatusPegawaiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-status-pegawai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.StatusPegawaiListForm = Result.Data.ToObject<ObservableCollection<MasterStatusPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
