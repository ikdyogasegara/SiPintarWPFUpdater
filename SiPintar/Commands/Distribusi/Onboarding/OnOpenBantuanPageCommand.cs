using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Distribusi;

namespace SiPintar.Commands.Distribusi.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly ViewModels.DistribusiViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, ViewModels.DistribusiViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(DistribusiViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
