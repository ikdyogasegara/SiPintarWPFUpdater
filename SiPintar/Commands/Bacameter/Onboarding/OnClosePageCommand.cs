using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Onboarding
{
    public class OnClosePageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly bool _isTest;

        public OnClosePageCommand(OnboardingViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.CurrentPageName = null;
            _viewModel.IsOverlayActive = false;
            await SetConfigAsync();
            DialogHelper.CloseDialog(_isTest, true, "BacameterRootDialog");
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private async Task SetConfigAsync()
        {
            if (!_isTest) { await LiteDBExtension.SetToAppConfigAsync("OnBoardBacameterFlag", "1"); }
        }
    }
}
