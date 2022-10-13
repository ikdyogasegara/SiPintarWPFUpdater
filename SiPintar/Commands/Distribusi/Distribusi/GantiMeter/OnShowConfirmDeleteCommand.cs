using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.Views.Distribusi.Distribusi.GantiMeter.KelainanBacameter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnShowConfirmDeleteCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnShowConfirmDeleteCommand(GantiMeterViewModel viewModel, bool isTest = false)
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
