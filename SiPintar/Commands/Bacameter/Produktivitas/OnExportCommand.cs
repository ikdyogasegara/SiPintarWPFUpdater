using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Produktivitas
{
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly ProduktivitasViewModel _viewModel;
        private readonly bool _isTest;

        public OnExportCommand(ProduktivitasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            await Task.FromResult(_isTest);
        }
    }
}
