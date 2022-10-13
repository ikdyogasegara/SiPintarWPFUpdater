using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin
{
    [ExcludeFromCodeCoverage]
    public class OnAddRotasiMeterCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnAddRotasiMeterCommand(GantiMeterNonRutinViewModel viewModel, bool isTest = false)
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
