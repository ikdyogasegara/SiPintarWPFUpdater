using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;

        public OnNextPageCommand(DataPembacaanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.GetDataListCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
