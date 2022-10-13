using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePermohonan
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly TipePermohonanViewModel _viewModel;

        public OnNextPageCommand(TipePermohonanViewModel viewModel)
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
