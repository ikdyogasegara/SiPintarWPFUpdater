using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.MutasiJabatan;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnOpenAddDetailFormCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddDetailFormCommand(MutasiJabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPegawai == null)
                return;

            _viewModel.IsLoading = true;
            _viewModel.IsEditDetail = false;
            _viewModel.IsUbahNik = false;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");
            await GetJabatanAsync();
            await GetDepartemenAsync();
            await GetAreaKerjaAsync();
            await GetDivisiAsync();

            _viewModel.FormDetailData = new MutasiJabatanDetailWpf
            {
                NoPegawai = _viewModel.SelectedDataPegawai.NoPegawai,
                NamaPegawai = _viewModel.SelectedDataPegawai.NamaPegawai,
                Jabatan = _viewModel.SelectedDataPegawai.Jabatan,
                Departemen = _viewModel.SelectedDataPegawai.Departemen,
                Divisi = _viewModel.SelectedDataPegawai.Divisi,
                AreaKerja = _viewModel.SelectedDataPegawai.AreaKerja,
                Tugas = _viewModel.SelectedDataPegawai.Tugas,
                KodeGolonganPegawai = _viewModel.SelectedDataPegawai.KodeGolonganPegawai,
                GolonganPegawai = _viewModel.SelectedDataPegawai.GolonganPegawai,
                Pendidikan = _viewModel.SelectedDataPegawai.Pendidikan,

                IdPegawai = _viewModel.SelectedDataPegawai.IdPegawai,
                IdJabatan = _viewModel.SelectedDataPegawai.IdJabatan,
                IdDepartemen = _viewModel.SelectedDataPegawai.IdDepartemen,
                IdDivisi = _viewModel.SelectedDataPegawai.IdDivisi,
                IdAreaKerja = _viewModel.SelectedDataPegawai.IdAreaKerja,
                IdGolonganPegawai = _viewModel.SelectedDataPegawai.IdGolonganPegawai,
                IdPendidikan = _viewModel.SelectedDataPegawai.IdPendidikan,

                IdJabatanBaru = null,
                IdDepartemenBaru = null,
                IdDivisiBaru = null,
                IdAreaKerjaBaru = null,
                TugasBaru = null,
            };

            _viewModel.IsLoading = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogDetailFormView(_viewModel));

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
