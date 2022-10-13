using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Perencanaan;

namespace SiPintar.Commands.Perencanaan.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly ViewModels.PerencanaanViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, ViewModels.PerencanaanViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(PerencanaanViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
