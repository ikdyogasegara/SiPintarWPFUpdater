using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnToggleUbahNikCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;

        public OnToggleUbahNikCommand(SKCalonPegawaiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsUbahNik = !_viewModel.IsUbahNik;

            if (!_viewModel.IsUbahNik)
            {
                var formDetailData = (MutasiStatusCapegDetailDto)_viewModel.FormDetailData.Clone();
                formDetailData.NoPegawaiBaru = _viewModel.FormDetailData.NoPegawai;
                _viewModel.FormDetailData = formDetailData;
            }

            await Task.FromResult(true);
        }
    }
}
