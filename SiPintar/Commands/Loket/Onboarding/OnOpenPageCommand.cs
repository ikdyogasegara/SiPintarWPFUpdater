using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Loket.Onboarding;

namespace SiPintar.Commands.Loket.Onboarding
{
    public class OnOpenPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly LoketViewModel _mainViewModel;
        private readonly bool _isTest;

        public OnOpenPageCommand(OnboardingViewModel viewModel, LoketViewModel mainVM, bool isTest = false)
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
            string Identifier = "LoketRootDialog";

            switch (pageName)
            {
                case "welcome":
                    _viewModel.PageTotal = 6;
                    onboardPage = !_isTest ? new WelcomeView(_viewModel) : null;
                    basePage = LoketViewType.Tagihan;
                    childPage = "Pelanggan SR";
                    break;
                case "tagihan_sr":
                    _viewModel.PageTotal = 6;
                    basePage = LoketViewType.Tagihan;
                    childPage = "Pelanggan SR";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "kolektif_air":
                    _viewModel.PageTotal = 7;
                    basePage = LoketViewType.Tagihan;
                    childPage = "Kolektif Air";
                    _viewModel.IsOverlayActive = true;
                    _viewModel.OnboardFigure = null;
                    break;
                case "angsuran":
                    _viewModel.PageTotal = 5;
                    basePage = LoketViewType.Angsuran;
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
