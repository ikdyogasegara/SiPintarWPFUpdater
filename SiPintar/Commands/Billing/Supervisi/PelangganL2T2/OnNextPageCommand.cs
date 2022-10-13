using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;

        public OnNextPageCommand(PelangganL2T2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
