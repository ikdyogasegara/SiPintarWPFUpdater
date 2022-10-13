using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.Posting
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _viewModel;

        public OnNextPageCommand(PostingViewModel viewModel)
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

