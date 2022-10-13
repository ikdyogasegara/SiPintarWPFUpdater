using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Billing;
using SiPintar.Views.Billing.Onboarding;

namespace SiPintar.Commands.Billing.Onboarding
{
    public class OnOpenDialogCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly BillingViewModel _parentViewModel;
        private readonly bool _isTest;

        public OnOpenDialogCommand(OnboardingViewModel viewModel, BillingViewModel parentViewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _parentViewModel = parentViewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var pageName = parameter as string;

            _viewModel.CurrentPageName = pageName;
            _viewModel.CurrentPageIndex = 1;

            dynamic onboardPage = null;
            dynamic basePage = null;
            dynamic childPage = null;

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 4;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = BillingViewType.Supervisi;
                    childPage = "Supervisi";
                    break;
                case "supervisi":
                    _viewModel.PageTotal = 4;
                    basePage = BillingViewType.Supervisi;
                    childPage = "Supervisi";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "atribut":
                    _viewModel.PageTotal = 5;
                    basePage = BillingViewType.Atribut;
                    childPage = "Atribut";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                default:
                    break;
            }

            _ = childPage;

            if (basePage != null)
                _parentViewModel.UpdateCurrentViewModelCommand.Execute(basePage);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "BillingRootDialog", () => onboardPage);
        }
    }
}
