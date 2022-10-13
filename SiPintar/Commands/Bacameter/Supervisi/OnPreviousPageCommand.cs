using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;

        public OnPreviousPageCommand(SupervisiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.GetListCommand.Execute(null);
            }

            await Task.FromResult(true);
        }


    }
}
