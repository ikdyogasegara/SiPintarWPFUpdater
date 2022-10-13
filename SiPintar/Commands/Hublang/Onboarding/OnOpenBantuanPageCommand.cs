using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Commands.Hublang.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly HublangViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, HublangViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(HublangViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
