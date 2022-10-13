using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitAddCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitAddCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsProcessAsBackgroundForm)
                _viewModel.OnSubmitAddAsBackgroundCommand.Execute(null);
            else
                _viewModel.OnSubmitAddAsNormalCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
