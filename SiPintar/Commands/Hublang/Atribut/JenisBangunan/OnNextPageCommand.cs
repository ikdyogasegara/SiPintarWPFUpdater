using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.JenisBangunan
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly JenisBangunanViewModel _viewModel;

        public OnNextPageCommand(JenisBangunanViewModel viewModel)
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
