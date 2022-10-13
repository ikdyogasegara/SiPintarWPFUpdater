using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.PegawaiList = new ObservableCollection<MasterPegawaiDto>();

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize", _viewModel.LimitData.Key },
                { "CurrentPage", _viewModel.CurrentPage }
            };

            if (_viewModel.IsPegawaiNonAktifChecked)
                param.Add("TampilkanPegawaiNonAktif", true);
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
            if (_viewModel.IsKawinChecked)
                param.Add("IdKawin", _viewModel.FilterKawin);
            if (_viewModel.IsPendidikanChecked)
                param.Add("IdPendidikan", _viewModel.FilterPendidikan);
            if (_viewModel.IsStatusPegawaiChecked)
                param.Add("IdStatusPegawai", _viewModel.FilterStatusPegawai);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PegawaiList = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.PegawaiList.Any();

            await GetAgamaAsync();
            await GetAreaKerjaAsync();
            await GetDepartemenAsync();
            await GetDivisiAsync();
            await GetGolonganAsync();
            await GetJabatanAsync();
            await GetJenisKelaminAsync();
            await GetPendidikanAsync();
            await GetStatusAsync();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
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

        public async Task GetStatusAsync()
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
