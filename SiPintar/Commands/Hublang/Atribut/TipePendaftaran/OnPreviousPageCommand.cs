using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePendaftaran
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly TipePendaftaranViewModel _viewModel;

        public OnPreviousPageCommand(TipePendaftaranViewModel viewModel)
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
