using System.Threading.Tasks;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Personalia;

namespace SiPintar.Commands.Personalia.Onboarding
{
    public class OnOpenBantuanPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly PersonaliaViewModel _mainViewModel;

        public OnOpenBantuanPageCommand(OnboardingViewModel viewModel, PersonaliaViewModel mainVM)
        {
            _viewModel = viewModel;
            _mainViewModel = mainVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ClosePageCommand.Execute(null);

            _mainViewModel.UpdateCurrentViewModelCommand.Execute(PersonaliaViewType.Bantuan);

            await Task.FromResult(true);
        }
    }
}
