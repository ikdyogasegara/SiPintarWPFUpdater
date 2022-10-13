using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;

        public OnToggleFilterCommand(TunjanganBulananViewModel viewModel)
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
