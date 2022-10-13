using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly LoketViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, LoketViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(LoketViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
