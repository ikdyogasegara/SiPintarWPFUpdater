using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.Views.Distribusi.Distribusi.GantiMeter.RotasiMeter;


namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    public class OnAddRotasiMeterCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnAddRotasiMeterCommand(GantiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = ((AsyncCommandBase)_viewModel.GetPelangganListCommand).ExecuteAsync(null);
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
        }

        private object GetInstance() => new DialogFormAddRotasiMeterView(_viewModel);
    }
}
