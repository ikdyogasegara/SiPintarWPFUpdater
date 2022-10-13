using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Gudang;
using SiPintar.Views.Gudang.Onboarding;

namespace SiPintar.Commands.Gudang.Onboarding
{
    public class OnCloseDialogPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly bool _isTest;

        public OnCloseDialogPageCommand(OnboardingViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.TempCurrentPageName = _viewModel.CurrentPageName;
                _viewModel.CurrentPageName = null;
                _viewModel.IsOverlayActive = false;

                DialogHost.Close("GudangRootDialog");
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message.ToString());
                Debug.WriteLine(e);
            }

            if (!_isTest)
                await DialogHost.Show(new CloseView(_viewModel), "GudangRootDialog");
        }
    }
}
