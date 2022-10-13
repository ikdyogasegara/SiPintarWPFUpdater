using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Personalia;
using SiPintar.Views.Personalia.Onboarding;

namespace SiPintar.Commands.Personalia.Onboarding
{
    public class OnOpenPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly PersonaliaViewModel _mainViewModel;
        private readonly bool _isTest;

        public OnOpenPageCommand(OnboardingViewModel viewModel, PersonaliaViewModel mainVM, bool isTest = false)
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
            string Identifier = "PersonaliaRootDialog";

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 6;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = PersonaliaViewType.DataMaster;
                    childPage = "Menu 1";
                    break;
                case "menu_1":
                    _viewModel.PageTotal = 6;
                    basePage = PersonaliaViewType.Kepegawaian;
                    childPage = "Menu 1";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "menu_2":
                    _viewModel.PageTotal = 7;
                    basePage = PersonaliaViewType.GajiPokok;
                    childPage = "Menu 2";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "menu_3":
                    _viewModel.PageTotal = 5;
                    basePage = PersonaliaViewType.Tunjangan;
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
