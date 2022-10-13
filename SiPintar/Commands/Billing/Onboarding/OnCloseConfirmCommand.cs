using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Billing;
using SiPintar.Views.Billing.Onboarding;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnCloseConfirmCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly bool _isTest;

        public OnCloseConfirmCommand(OnboardingViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
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

            if (!_isTest) await DialogHost.Show(new CloseView(_viewModel), "BillingRootDialog");

            await Task.FromResult(true);
        }
    }
}
