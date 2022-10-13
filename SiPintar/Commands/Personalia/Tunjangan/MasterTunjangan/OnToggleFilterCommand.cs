using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;

        public OnToggleFilterCommand(MasterTunjanganViewModel viewModel)
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
