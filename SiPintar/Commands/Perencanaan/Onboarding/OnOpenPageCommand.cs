using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Perencanaan;
using SiPintar.Views.Perencanaan.Onboarding;

namespace SiPintar.Commands.Perencanaan.Onboarding
{
    public class OnOpenPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly ViewModels.PerencanaanViewModel _mainViewModel;
        private readonly bool _isTest;

        public OnOpenPageCommand(OnboardingViewModel viewModel, ViewModels.PerencanaanViewModel mainVM, bool isTest = false)
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
            string Identifier = "PerencanaanRootDialog";

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 6;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = PerencanaanViewType.Perencanaan;
                    childPage = "Perencanaan";
                    break;
                case "perencanaan":
                    _viewModel.PageTotal = 6;
                    basePage = PerencanaanViewType.Perencanaan;
                    childPage = "Perencanaan";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "supervisi":
                    _viewModel.PageTotal = 7;
                    basePage = PerencanaanViewType.Atribut;
                    childPage = "Menu 2";
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
