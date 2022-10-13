using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Onboarding
{
    public class OnCloseBackPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnCloseBackPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.OpenPageCommand.Execute(_viewModel.TempCurrentPageName);

                DialogHost.Close("BacameterRootDialog");
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
