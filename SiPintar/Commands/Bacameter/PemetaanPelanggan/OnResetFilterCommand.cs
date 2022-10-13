using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.PemetaanPelanggan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PemetaanPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(PemetaanPelangganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SelectedJenisPeta = null;
            _viewModel.IsRayonChecked = false;
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
