using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class OnResetCommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetCommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FileName = null;
            _viewModel.Content = null;

            await Task.FromResult(_isTest);
        }
    }
}
