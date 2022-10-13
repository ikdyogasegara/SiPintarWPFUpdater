using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;

        public OnPreviousPageCommand(DataPembacaanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.GetDataListCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
