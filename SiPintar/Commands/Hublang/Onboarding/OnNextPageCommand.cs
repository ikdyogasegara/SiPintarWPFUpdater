using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Commands.Hublang.Onboarding
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnNextPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPageIndex < _viewModel.PageTotal)
            {
                _viewModel.CurrentPageIndex++;
                _viewModel.LoadFigureCommand.Execute(null);
            }
            else
                _viewModel.ClosePageCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
