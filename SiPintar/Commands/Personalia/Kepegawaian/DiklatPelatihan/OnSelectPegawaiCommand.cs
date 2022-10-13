using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnSelectPegawaiCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnSelectPegawaiCommand(DiklatPelatihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormDetailData = new DiklatDetailDto
            {
                IdPegawai = _viewModel.SelectedDataPegawai.IdPegawai,
                NoPegawai = _viewModel.SelectedDataPegawai.NoPegawai,
                NamaPegawai = _viewModel.SelectedDataPegawai.NamaPegawai,
                KodeGolonganPegawai = _viewModel.SelectedDataPegawai.KodeGolonganPegawai,
                Jabatan = _viewModel.SelectedDataPegawai.Jabatan,
                Departemen = _viewModel.SelectedDataPegawai.Departemen,
                Divisi = _viewModel.SelectedDataPegawai.Divisi,
                AreaKerja = _viewModel.SelectedDataPegawai.AreaKerja,
                Tugas = _viewModel.SelectedDataPegawai.Tugas,
            };

            _viewModel.FormDetailList.Add((DiklatDetailDto)_viewModel.FormDetailData.Clone());

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
