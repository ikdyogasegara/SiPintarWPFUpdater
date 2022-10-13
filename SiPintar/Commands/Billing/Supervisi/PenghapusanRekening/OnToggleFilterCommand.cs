using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;

        public OnToggleFilterCommand(PenghapusanRekeningViewModel viewModel)
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
