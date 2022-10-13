using System.Threading.Tasks;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnNextPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPageIndex < (_viewModel.PageTotal - 1))
            {
                _viewModel.CurrentPageIndex++;
            }

            await Task.FromResult(true);
        }
    }
}
