using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Onboarding;

namespace SiPintar.Commands.Bacameter.Onboarding
{
    public class OnOpenPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly BacameterViewModel _mainViewModel;
        private readonly bool _isTest;

        public OnOpenPageCommand(OnboardingViewModel viewModel, BacameterViewModel mainVM, bool isTest = false)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
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
            string Identifier = "BacameterRootDialog";

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 6;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = BacameterViewType.Supervisi;
                    childPage = "Supervisi";
                    break;
                case "supervisi":
                    _viewModel.PageTotal = 6;
                    basePage = BacameterViewType.Supervisi;
                    childPage = "Supervisi";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "produktivitas":
                    _viewModel.PageTotal = 7;
                    basePage = BacameterViewType.Produktivitas;
                    childPage = "Produktivitas";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                default:
                    break;
            }

            _ = childPage;

            if (basePage != null)
                _mainViewModel.UpdateCurrentViewModelCommand.Execute(basePage);

            await OpenDialogAsync(onboardPage, Identifier);
        }

        [ExcludeFromCodeCoverage]
        private async Task OpenDialogAsync(dynamic onboardPage, string Identifier)
        {
            if (onboardPage != null && !DialogHost.IsDialogOpen(Identifier))
                await DialogHost.Show(onboardPage, Identifier);
        }
    }
}
