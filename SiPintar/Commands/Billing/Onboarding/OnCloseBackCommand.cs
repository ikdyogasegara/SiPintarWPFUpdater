using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnCloseBackCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnCloseBackCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                DialogHost.Close("BillingRootDialog");
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message.ToString());
                Debug.WriteLine(e);
            }

            _viewModel.OnOpenDialogCommand.Execute(_viewModel.CurrentPageIndex);

            await Task.FromResult(true);
        }
    }
}
