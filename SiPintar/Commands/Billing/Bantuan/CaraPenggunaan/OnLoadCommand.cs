using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Bantuan;

namespace SiPintar.Commands.Billing.Bantuan.CaraPenggunaan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly CaraPenggunaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(CaraPenggunaanViewModel viewModel, bool isTest = false)
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
