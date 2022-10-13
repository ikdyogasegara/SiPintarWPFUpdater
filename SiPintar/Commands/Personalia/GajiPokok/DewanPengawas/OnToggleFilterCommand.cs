using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.DewanPengawas
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly DewanPengawasViewModel _viewModel;

        public OnToggleFilterCommand(DewanPengawasViewModel viewModel)
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
