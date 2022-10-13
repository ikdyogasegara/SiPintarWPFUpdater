using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.MutasiJabatan;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnOpenEditDetailFormCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditDetailFormCommand(MutasiJabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDetailData != null)
            {
                _viewModel.IsLoading = true;
                _viewModel.IsEditDetail = true;
                _viewModel.IsUbahNik = false;

                var jabatanBaru = _viewModel.SelectedDetailData.IdJabatanBaru;
                var departemenBaru = _viewModel.SelectedDetailData.IdDepartemenBaru;
                var areaKerjaBaru = _viewModel.SelectedDetailData.IdAreaKerjaBaru;
                var divisiBaru = _viewModel.SelectedDetailData.IdDivisiBaru;
                await GetJabatanAsync();
                await GetDepartemenAsync();
                await GetAreaKerjaAsync();
                await GetDivisiAsync();

                _viewModel.FormDetailData = new MutasiJabatanDetailWpf
                {
                    NoPegawai = _viewModel.SelectedDetailData.NoPegawai,
                    NamaPegawai = _viewModel.SelectedDetailData.NamaPegawai,
                    Jabatan = _viewModel.SelectedDetailData.Jabatan,
                    Departemen = _viewModel.SelectedDetailData.Departemen,
                    Divisi = _viewModel.SelectedDetailData.Divisi,
                    AreaKerja = _viewModel.SelectedDetailData.AreaKerja,
                    Tugas = _viewModel.SelectedDetailData.Tugas,
                    KodeGolonganPegawai = _viewModel.SelectedDetailData.KodeGolonganPegawai,
                    GolonganPegawai = _viewModel.SelectedDetailData.GolonganPegawai,
                    Pendidikan = _viewModel.SelectedDetailData.Pendidikan,

                    IdPegawai = _viewModel.SelectedDetailData.IdPegawai,
                    IdJabatan = _viewModel.SelectedDetailData.IdJabatan,
                    IdDepartemen = _viewModel.SelectedDetailData.IdDepartemen,
                    IdDivisi = _viewModel.SelectedDetailData.IdDivisi,
                    IdAreaKerja = _viewModel.SelectedDetailData.IdAreaKerja,
                    IdGolonganPegawai = _viewModel.SelectedDetailData.IdGolonganPegawai,
                    IdPendidikan = _viewModel.SelectedDetailData.IdPendidikan,

                    IdJabatanBaru = jabatanBaru,
                    IdDepartemenBaru = departemenBaru,
                    IdDivisiBaru = divisiBaru,
                    IdAreaKerjaBaru = areaKerjaBaru,
                    TugasBaru = _viewModel.SelectedDetailData.TugasBaru,
                };

                _viewModel.IsLoading = false;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogDetailFormView(_viewModel));
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerDialog",
                    "Koreksi Pegawai",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            }
            await Task.FromResult(_isTest);
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
                    _viewModel.JabatanFormList = Result.Data.ToObject<ObservableCollection<MasterJabatanDto>>();
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
                    _viewModel.DepartemenFormList = Result.Data.ToObject<ObservableCollection<MasterDepartemenDto>>();
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
                    _viewModel.AreaKerjaFormList = Result.Data.ToObject<ObservableCollection<MasterAreaKerjaDto>>();
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
                    _viewModel.DivisiFormList = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

    }
}
