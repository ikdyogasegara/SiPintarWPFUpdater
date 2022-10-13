using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnCloseDialogCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnCloseDialogCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            try
            {
                await LiteDBExtension.SetToAppConfigAsync("OnBoardBillingFlag", "1");

                DialogHost.Close("BillingRootDialog");
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
