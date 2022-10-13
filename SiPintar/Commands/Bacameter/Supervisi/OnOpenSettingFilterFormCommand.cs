using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenSettingFilterFormCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingFilterFormCommand(SupervisiViewModel viewModel, bool isTest = false)
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
