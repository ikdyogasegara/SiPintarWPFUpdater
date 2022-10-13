using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Ongkos
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly OngkosViewModel _viewModel;

        public OnPreviousPageCommand(OngkosViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.OnFilterCommand.Execute(null);
            }

            await Task.FromResult(true);
        }
    }
}
