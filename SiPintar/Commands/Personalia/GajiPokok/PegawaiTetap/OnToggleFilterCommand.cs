using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.PegawaiTetap
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly PegawaiTetapViewModel _viewModel;

        public OnToggleFilterCommand(PegawaiTetapViewModel viewModel)
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
