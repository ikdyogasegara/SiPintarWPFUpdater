using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(MutasiJabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>();

            if (_viewModel.IsNoPegawaiChecked)
                param.Add("NoPegawai", _viewModel.FilterNoPegawai);
            if (_viewModel.IsNamaPegawaiChecked)
                param.Add("NamaPegawai", _viewModel.FilterNamaPegawai);
            if (_viewModel.IsStatusPegawaiChecked)
                param.Add("IdStatusPegawai", _viewModel.FilterStatusPegawai);
            if (_viewModel.IsJabatanChecked)
                param.Add("IdJabatan", _viewModel.FilterJabatan);
            if (_viewModel.IsDepartemenChecked)
                param.Add("IdDepartemen", _viewModel.FilterDepartemen);
            if (_viewModel.IsDivisiChecked)
                param.Add("IdDivisi", _viewModel.FilterDivisi);
            if (_viewModel.IsAreaKerjaChecked)
                param.Add("IdAreaKerja", _viewModel.FilterAreaKerja);
            if (_viewModel.IsJabatanBaruChecked)
                param.Add("IdJabatanBaru", _viewModel.FilterJabatanBaru);
            if (_viewModel.IsDepartemenBaruChecked)
                param.Add("IdDepartemenBaru", _viewModel.FilterDepartemenBaru);
            if (_viewModel.IsDivisiBaruChecked)
                param.Add("IdDivisiBaru", _viewModel.FilterDivisiBaru);
            if (_viewModel.IsAreaKerjaBaruChecked)
                param.Add("IdAreaKerjaBaru", _viewModel.FilterAreaKerjaBaru);

            var filterNoPegawai = _viewModel.FilterNoPegawai;
            var filterNamaPegawai = _viewModel.FilterNamaPegawai;
            var filterStatusPegawai = _viewModel.FilterStatusPegawai;
            var filterJabatan = _viewModel.FilterJabatan;
            var filterDepartemen = _viewModel.FilterDepartemen;
            var filterDivisi = _viewModel.FilterDivisi;
            var filterAreaKerja = _viewModel.FilterAreaKerja;
            var filterJabatanBaru = _viewModel.FilterJabatanBaru;
            var filterDepartemenBaru = _viewModel.FilterDepartemenBaru;
            var filterDivisiBaru = _viewModel.FilterDivisiBaru;
            var filterAreaKerjaBaru = _viewModel.FilterAreaKerjaBaru;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/mutasi-jabatan", param));
            _viewModel.MutasiJabatanList = new ObservableCollection<MutasiJabatanDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.MutasiJabatanList = Result.Data.ToObject<ObservableCollection<MutasiJabatanDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.MutasiJabatanList.Any();

            await GetStatusPegawaiAsync();
            await GetAreaKerjaAsync();
            await GetDepartemenAsync();
            await GetDivisiAsync();
            await GetJabatanAsync();

            _viewModel.FilterNoPegawai = filterNoPegawai;
            _viewModel.FilterNamaPegawai = filterNamaPegawai;
            _viewModel.FilterStatusPegawai = filterStatusPegawai;
            _viewModel.FilterJabatan = filterJabatan;
            _viewModel.FilterDepartemen = filterDepartemen;
            _viewModel.FilterDivisi = filterDivisi;
            _viewModel.FilterAreaKerja = filterAreaKerja;
            _viewModel.FilterJabatanBaru = filterJabatanBaru;
            _viewModel.FilterDepartemenBaru = filterDepartemenBaru;
            _viewModel.FilterDivisiBaru = filterDivisiBaru;
            _viewModel.FilterAreaKerjaBaru = filterAreaKerjaBaru;

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
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
    }
}
