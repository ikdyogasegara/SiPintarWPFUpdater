using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Akuntansi;
using SiPintar.Views.Akuntansi.Onboarding;

namespace SiPintar.Commands.Akuntansi.Onboarding
{
    public class OnOpenPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly AkuntansiViewModel _mainViewModel;
        private readonly bool _isTest;

        public OnOpenPageCommand(OnboardingViewModel viewModel, AkuntansiViewModel mainVM, bool isTest = false)
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
            string Identifier = "AkuntansiRootDialog";

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 6;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = AkuntansiViewType.Voucher;
                    childPage = "Voucher";
                    break;
                case "voucher":
                    _viewModel.PageTotal = 6;
                    basePage = AkuntansiViewType.Voucher;
                    childPage = "Voucher";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "postingAkuntansi":
                    _viewModel.PageTotal = 7;
                    basePage = AkuntansiViewType.PostingAkuntansi;
                    childPage = "PostingAkuntansi";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "jurnal":
                    _viewModel.PageTotal = 5;
                    basePage = AkuntansiViewType.Jurnal;
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
