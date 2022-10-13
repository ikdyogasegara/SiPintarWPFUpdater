using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.CalonPegawai
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly CalonPegawaiViewModel _viewModel;

        public OnToggleFilterCommand(CalonPegawaiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFilterVisible = !_viewModel.IsFilterVisible;

            await Task.FromResult(true);
        }
    }
}
