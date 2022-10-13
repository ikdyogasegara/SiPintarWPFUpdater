using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;

        public OnToggleFilterCommand(PelangganL2T2ViewModel viewModel)
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
