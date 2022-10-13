using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin
{
    [ExcludeFromCodeCoverage]
    public class OnShowConfirmDeleteCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnShowConfirmDeleteCommand(GantiMeterNonRutinViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = ((AsyncCommandBase)_viewModel.RotasiMeter.OnOpenDeleteFormCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
