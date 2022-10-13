using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Gudang;

namespace SiPintar.Commands.Gudang.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly GudangViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, GudangViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(GudangViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
