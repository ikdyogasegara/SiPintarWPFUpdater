using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;

        public OnPreviousPageCommand(RekeningL2T2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(true);
        }
    }
}
