using System.Threading.Tasks;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnPreviousPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPageIndex > 0)
            {
                _viewModel.CurrentPageIndex--;
            }

            await Task.FromResult(true);
        }
    }
}
