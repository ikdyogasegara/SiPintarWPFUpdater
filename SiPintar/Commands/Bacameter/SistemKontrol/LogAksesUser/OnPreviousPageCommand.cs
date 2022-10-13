using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.LogAksesUser
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly LogAksesUserViewModel _viewModel;

        public OnPreviousPageCommand(LogAksesUserViewModel viewModel)
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
