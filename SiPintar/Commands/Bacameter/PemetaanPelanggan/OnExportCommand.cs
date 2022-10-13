using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.PemetaanPelanggan
{
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly PemetaanPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnExportCommand(PemetaanPelangganViewModel viewModel, bool isTest = false)
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
