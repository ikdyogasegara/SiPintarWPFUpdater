using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi;

namespace SiPintar.Commands.Distribusi.Onboarding
{
    public class OnClosePageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnClosePageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.CurrentPageName = null;
                _viewModel.IsOverlayActive = false;

                await LiteDBExtension.SetToAppConfigAsync("OnBoardDistribusiFlag", "1");

                DialogHost.Close("DistribusiRootDialog");
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message.ToString());
                Debug.WriteLine(e);
            }

            await Task.FromResult(true);
        }
    }
}
