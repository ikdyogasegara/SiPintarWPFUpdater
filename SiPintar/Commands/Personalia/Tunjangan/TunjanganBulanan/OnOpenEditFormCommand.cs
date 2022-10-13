using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.TunjanganBulanan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TunjanganBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = true;
            if (_viewModel.SelectedData != null)
            {
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

                _viewModel.FormJenisTunjangan = _viewModel.SelectedData.IdJenisTunjangan;
                _viewModel.FormFlagAbsensi = _viewModel.SelectedData.FlagAbsensi;
                _viewModel.FormUpahKehadiran = _viewModel.SelectedData.IdUpahKehadiran;
                _viewModel.FormNominal = _viewModel.SelectedData.Nominal;

                _viewModel.FormBeban = "Pegawai";
                _viewModel.FormFlagPersen = _viewModel.SelectedData.FlagPersen;
                _viewModel.FormPersenDari = _viewModel.SelectedData.PersenDari;
                _viewModel.FormNominalMin = _viewModel.SelectedData.NominalMin;
                _viewModel.FormNominalMax = _viewModel.SelectedData.NominalMax;

                _viewModel.FormKeterangan = _viewModel.SelectedData.Keterangan;
                _viewModel.FormFlagPotongPph = _viewModel.SelectedData.FlagPotongPph;
                _viewModel.FormFlagPersenPph = _viewModel.SelectedData.FlagPersenPph;
                _viewModel.FormNominalPph = _viewModel.SelectedData.NominalPph;

                _viewModel.FormFlagSemuaPegawai = _viewModel.SelectedData.FlagSemuaPegawai;

                _viewModel.FormAgama = _viewModel.SelectedData.IdAgama;
                _viewModel.FormDivisi = _viewModel.SelectedData.IdDivisi;
                _viewModel.FormJabatan = _viewModel.SelectedData.IdJabatan;
                _viewModel.FormPendidikan = _viewModel.SelectedData.IdPendidikan;
                _viewModel.FormAreaKerja = _viewModel.SelectedData.IdAreaKerja;
                _viewModel.FormGolonganPegawai = _viewModel.SelectedData.IdGolonganPegawai;
                _viewModel.FormJenisKelamin = _viewModel.SelectedData.IdJenisKelamin;
                _viewModel.FormTipeKeluarga = _viewModel.SelectedData.IdTipeKeluarga;
                _viewModel.FormDepartemen = _viewModel.SelectedData.IdDepartemen;
                _viewModel.FormMkg = _viewModel.SelectedData.Mkg;
                _viewModel.FormPegawai = _viewModel.SelectedData.IdPegawai;
                _viewModel.FormStatusPegawai = _viewModel.SelectedData.IdStatusPegawai;

                _viewModel.FormIsAgamaChecked = _viewModel.SelectedData.IdAgama != null;
                _viewModel.FormIsDivisiChecked = _viewModel.SelectedData.IdDivisi != null;
                _viewModel.FormIsJabatanChecked = _viewModel.SelectedData.IdJabatan != null;
                _viewModel.FormIsPendidikanChecked = _viewModel.SelectedData.IdPendidikan != null;
                _viewModel.FormIsAreaKerjaChecked = _viewModel.SelectedData.IdAreaKerja != null;
                _viewModel.FormIsGolonganPegawaiChecked = _viewModel.SelectedData.IdGolonganPegawai != null;
                _viewModel.FormIsJenisKelaminChecked = _viewModel.SelectedData.IdJenisKelamin != null;
                _viewModel.FormIsTipeKeluargaChecked = _viewModel.SelectedData.IdTipeKeluarga != null;
                _viewModel.FormIsDepartemenChecked = _viewModel.SelectedData.IdDepartemen != null;
                _viewModel.FormIsMkgChecked = _viewModel.SelectedData.Mkg != null;
                _viewModel.FormIsPegawaiChecked = _viewModel.SelectedData.IdPegawai != null;
                _viewModel.FormIsStatusPegawaiChecked = _viewModel.SelectedData.IdStatusPegawai != null;

                await GetPegawaiAsync();

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Tunjangan Bulanan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }

        public async Task GetPegawaiAsync()
        {
            _viewModel.IsLoading = true;
            var param = new Dictionary<string, dynamic>
            {
                { "IdPegawai", _viewModel.SelectedData.IdPegawai }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.SelectedDataPegawai = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>().FirstOrDefault();
                }
            }

            _viewModel.IsLoading = false;
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
