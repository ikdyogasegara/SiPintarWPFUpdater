using System.Threading.Tasks;
using SiPintar.ViewModels.Distribusi;

namespace SiPintar.Commands.Distribusi.Onboarding
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnPreviousPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPageIndex > 1)
            {
                _viewModel.CurrentPageIndex--;
                _viewModel.LoadFigureCommand.Execute(null);
            }

            await Task.FromResult(true);
        }
    }
}
