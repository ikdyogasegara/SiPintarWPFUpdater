using System.Threading.Tasks;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    public class OnShowSpkCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnShowSpkCommand(GantiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = ((AsyncCommandBase)_viewModel.RotasiMeter.OnOpenSpkSurveiCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
