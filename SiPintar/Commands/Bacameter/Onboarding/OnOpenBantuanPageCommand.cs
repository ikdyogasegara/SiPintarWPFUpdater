using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly BacameterViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, BacameterViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(BacameterViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
