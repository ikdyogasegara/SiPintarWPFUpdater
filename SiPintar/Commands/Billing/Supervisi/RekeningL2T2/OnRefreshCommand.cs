using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;

        public OnRefreshCommand(RekeningL2T2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.OnLoadCommand.Execute(true);

            await Task.FromResult(true);
        }
    }
}
