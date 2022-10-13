using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaHarian
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly TenagaHarianViewModel _viewModel;

        public OnToggleFilterCommand(TenagaHarianViewModel viewModel)
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
