using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKCalonPegawai;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnOpenAddDetailFormCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddDetailFormCommand(SKCalonPegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            await GetGolonganAsync();

            _viewModel.FormDetailData = new MutasiStatusCapegDetailDto
            {
                NamaPegawai = _viewModel.SelectedDataPegawai.NamaPegawai,
                Jabatan = _viewModel.SelectedDataPegawai.Jabatan,
                Departemen = _viewModel.SelectedDataPegawai.Departemen,
                Divisi = _viewModel.SelectedDataPegawai.Divisi,
                AreaKerja = _viewModel.SelectedDataPegawai.AreaKerja,
                Tugas = _viewModel.SelectedDataPegawai.Tugas,
                Pendidikan = _viewModel.SelectedDataPegawai.Pendidikan,

                IdPegawai = _viewModel.SelectedDataPegawai.IdPegawai,
                IdJabatan = _viewModel.SelectedDataPegawai.IdJabatan,
                IdDepartemen = _viewModel.SelectedDataPegawai.IdDepartemen,
                IdDivisi = _viewModel.SelectedDataPegawai.IdDivisi,
                IdAreaKerja = _viewModel.SelectedDataPegawai.IdAreaKerja,
                IdPendidikan = _viewModel.SelectedDataPegawai.IdPendidikan,

                NoPegawai = _viewModel.SelectedDataPegawai.NoPegawai,
                NoPegawaiBaru = _viewModel.SelectedDataPegawai.NoPegawai,
                IdGolonganPegawai = null,
                Mkg_Thn = 0,
                Mkg_Bln = 0,
            };

            _viewModel.IsLoading = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogDetailFormView(_viewModel));

            await Task.FromResult(_isTest);
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
                    _viewModel.GolonganFormList = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

    }
}
