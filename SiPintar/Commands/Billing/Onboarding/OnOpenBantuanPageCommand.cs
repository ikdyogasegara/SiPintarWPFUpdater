using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly BillingViewModel _parentViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, BillingViewModel parentVM)
        {
            _viewModel = viewModel;
            _parentViewModel = parentVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.OnCloseDialogCommand.Execute(null);

            _parentViewModel.UpdateCurrentViewModelCommand.Execute(BillingViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
