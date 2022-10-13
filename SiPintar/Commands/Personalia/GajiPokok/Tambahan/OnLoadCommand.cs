using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(TambahanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var filterPeriode = _viewModel.FilterPeriode;
            var filterKodeGaji = _viewModel.FilterKodeGaji;
            var filterNoPegawai = _viewModel.FilterNoPegawai;
            var filterNamaPegawai = _viewModel.FilterNamaPegawai;
            var filterAgama = _viewModel.FilterAgama;
            var filterAreaKerja = _viewModel.FilterAreaKerja;
            var filterDepartemen = _viewModel.FilterDepartemen;
            var filterDivisi = _viewModel.FilterDivisi;
            var filterJabatan = _viewModel.FilterJabatan;
            var filterGolonganPegawai = _viewModel.FilterGolonganPegawai;
            var filterJenisKelamin = _viewModel.FilterJenisKelamin;
            var filterPendidikan = _viewModel.FilterPendidikan;
            var filterTipeKeluarga = _viewModel.FilterTipeKeluarga;
            var filterStatusPegawai = _viewModel.FilterStatusPegawai;

            _viewModel.TambahanList = new ObservableCollection<MasterPegawaiGajiTambahanDto>();
            if (_viewModel.IsPilih)
            {
                var param = new Dictionary<string, dynamic>
                    {
                        { "KodePeriode", _viewModel.FilterPeriode },
                        { "IdKodeGaji", _viewModel.FilterKodeGaji }
                    };

                if (_viewModel.IsNoPegawaiChecked)
                    param.Add("NoPegawai", _viewModel.FilterNoPegawai);
                if (_viewModel.IsNamaPegawaiChecked)
                    param.Add("NamaPegawai", _viewModel.FilterNamaPegawai);
                if (_viewModel.IsAgamaChecked)
                    param.Add("IdAgama", _viewModel.FilterAgama);
                if (_viewModel.IsAreaKerjaChecked)
                    param.Add("IdAreaKerja", _viewModel.FilterAreaKerja);
                if (_viewModel.IsDepartemenChecked)
                    param.Add("IdDepartemen", _viewModel.FilterDepartemen);
                if (_viewModel.IsDivisiChecked)
                    param.Add("IdDivisi", _viewModel.FilterDivisi);
                if (_viewModel.IsGolonganPegawaiChecked)
                    param.Add("IdGolonganPegawai", _viewModel.FilterGolonganPegawai);
                if (_viewModel.IsJabatanChecked)
                    param.Add("IdJabatan", _viewModel.FilterJabatan);
                if (_viewModel.IsJenisKelaminChecked)
                    param.Add("IdJenisKelamin", _viewModel.FilterJenisKelamin);
                if (_viewModel.IsPendidikanChecked)
                    param.Add("IdPendidikan", _viewModel.FilterPendidikan);
                if (_viewModel.IsTipeKeluargaChecked)
                    param.Add("IdTipeKeluarga", _viewModel.FilterTipeKeluarga);
                if (_viewModel.IsStatusPegawaiChecked)
                    param.Add("IdStatusPegawai", _viewModel.FilterStatusPegawai);

                var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-gaji-pegawai-tambahan", param));
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status && Result.Data != null)
                    {
                        _viewModel.TambahanList = Result.Data.ToObject<ObservableCollection<MasterPegawaiGajiTambahanDto>>();
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            }
            _viewModel.IsEmpty = !_viewModel.TambahanList.Any();

            await GetPeriodeAsync();
            await GetKodeGajiAsync();
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

            _viewModel.FilterPeriode = filterPeriode ?? _viewModel.PeriodeList.FirstOrDefault(p => p.KodePeriode == ((DateTime.Today.Year * 100) + DateTime.Today.Month))?.IdPeriode ?? _viewModel.PeriodeList.FirstOrDefault()?.IdPeriode;
            _viewModel.FilterKodeGaji = filterKodeGaji;
            _viewModel.FilterNoPegawai = filterNoPegawai;
            _viewModel.FilterNamaPegawai = filterNamaPegawai;
            _viewModel.FilterAgama = filterAgama;
            _viewModel.FilterAreaKerja = filterAreaKerja;
            _viewModel.FilterDepartemen = filterDepartemen;
            _viewModel.FilterDivisi = filterDivisi;
            _viewModel.FilterGolonganPegawai = filterGolonganPegawai;
            _viewModel.FilterJabatan = filterJabatan;
            _viewModel.FilterJenisKelamin = filterJenisKelamin;
            _viewModel.FilterPendidikan = filterPendidikan;
            _viewModel.FilterTipeKeluarga = filterTipeKeluarga;
            _viewModel.FilterStatusPegawai = filterStatusPegawai;


            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeAsync()
        {
            _viewModel.IsLoading = true;
            var param = new Dictionary<string, dynamic> { { "Status", true } };
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetKodeGajiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kode-gaji");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KodeGajiList = Result.Data.ToObject<ObservableCollection<MasterKodeGajiDto>>();
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
                    _viewModel.AgamaList = Result.Data.ToObject<ObservableCollection<MasterAgamaDto>>();
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
                    _viewModel.AreaKerjaList = Result.Data.ToObject<ObservableCollection<MasterAreaKerjaDto>>();
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
                    _viewModel.DepartemenList = Result.Data.ToObject<ObservableCollection<MasterDepartemenDto>>();
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
                    _viewModel.DivisiList = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
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
                    _viewModel.GolonganPegawaiList = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
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
                    _viewModel.JabatanList = Result.Data.ToObject<ObservableCollection<MasterJabatanDto>>();
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
                    _viewModel.JenisKelaminList = Result.Data.ToObject<ObservableCollection<MasterJenisKelaminDto>>();
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
                    _viewModel.PendidikanList = Result.Data.ToObject<ObservableCollection<MasterPendidikanDto>>();
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
                    _viewModel.TipeKeluargaList = Result.Data.ToObject<ObservableCollection<MasterTipeKeluargaDto>>();
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
                    _viewModel.StatusPegawaiList = Result.Data.ToObject<ObservableCollection<MasterStatusPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
