using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;

        public OnNextPageCommand(MaterialViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.OnFilterCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
