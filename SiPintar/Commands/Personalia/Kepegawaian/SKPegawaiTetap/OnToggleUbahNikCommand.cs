using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnToggleUbahNikCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;

        public OnToggleUbahNikCommand(SKPegawaiTetapViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsUbahNik = !_viewModel.IsUbahNik;

            if (!_viewModel.IsUbahNik)
            {
                var formDetailData = (MutasiStatusTetapDetailDto)_viewModel.FormDetailData.Clone();
                formDetailData.NoPegawai = _viewModel.FormDetailData.NoPegawai;
                _viewModel.FormDetailData = formDetailData;
            }

            await Task.FromResult(true);
        }
    }
}
