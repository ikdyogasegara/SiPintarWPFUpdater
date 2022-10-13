using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Commands.Akuntansi.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly AkuntansiViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, AkuntansiViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(AkuntansiViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
