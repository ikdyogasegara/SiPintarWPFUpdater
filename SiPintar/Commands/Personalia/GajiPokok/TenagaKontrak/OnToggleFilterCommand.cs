using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaKontrak
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly TenagaKontrakViewModel _viewModel;

        public OnToggleFilterCommand(TenagaKontrakViewModel viewModel)
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
