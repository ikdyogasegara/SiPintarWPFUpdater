using System.Threading.Tasks;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;

        public OnNextPageCommand(DaftarPenggunaViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.OnRefreshCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
