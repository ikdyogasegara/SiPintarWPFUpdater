using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;

        public OnPreviousPageCommand(PelangganLimbahViewModel viewModel)
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
