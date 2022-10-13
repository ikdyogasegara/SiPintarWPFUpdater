using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKPegawaiTetap;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnOpenEditDetailFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditDetailFormCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

                _viewModel.FormDetailData = new MutasiStatusTetapDetailDto
                {
                    NamaPegawai = _viewModel.SelectedDetailData.NamaPegawai,
                    Jabatan = _viewModel.SelectedDetailData.Jabatan,
                    Departemen = _viewModel.SelectedDetailData.Departemen,
                    Divisi = _viewModel.SelectedDetailData.Divisi,
                    AreaKerja = _viewModel.SelectedDetailData.AreaKerja,
                    Tugas = _viewModel.SelectedDetailData.Tugas,
                    Pendidikan = _viewModel.SelectedDetailData.Pendidikan,

                    IdPegawai = _viewModel.SelectedDetailData.IdPegawai,
                    IdJabatan = _viewModel.SelectedDetailData.IdJabatan,
                    IdDepartemen = _viewModel.SelectedDetailData.IdDepartemen,
                    IdDivisi = _viewModel.SelectedDetailData.IdDivisi,
                    IdAreaKerja = _viewModel.SelectedDetailData.IdAreaKerja,
                    IdPendidikan = _viewModel.SelectedDetailData.IdPendidikan,
                    IdGolonganPegawai = _viewModel.SelectedDetailData.IdGolonganPegawai,

                    NoPegawai = _viewModel.SelectedDetailData.NoPegawai,
                    Mkg_Thn = _viewModel.SelectedDetailData.Mkg_Thn,
                    Mkg_Bln = _viewModel.SelectedDetailData.Mkg_Bln,
                };

                _viewModel.IsLoading = false;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogDetailFormView(_viewModel));
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerDialog",
                    "Koreksi Pegawai Tetap",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            }
            await Task.FromResult(_isTest);
        }

    }
}
