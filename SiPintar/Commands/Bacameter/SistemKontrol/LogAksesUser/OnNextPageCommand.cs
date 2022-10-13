using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.LogAksesUser
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly LogAksesUserViewModel _viewModel;

        public OnNextPageCommand(LogAksesUserViewModel viewModel)
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
