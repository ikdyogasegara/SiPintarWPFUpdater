using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;

        public OnToggleFilterCommand(PengecualianTunjanganViewModel viewModel)
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
