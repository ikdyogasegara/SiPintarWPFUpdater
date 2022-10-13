using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;

        public OnRefreshCommand(RekeningLimbahViewModel viewModel)
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
