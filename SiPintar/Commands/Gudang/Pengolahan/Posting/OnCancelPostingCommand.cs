using System.Threading;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.Commands.Gudang.Pengolahan.Posting
{
    public class OnCancelPostingCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _vm;
        private readonly bool _isTest;

        public OnCancelPostingCommand(PostingViewModel viewModel, bool isTest = false)
        {
            _vm = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.CancelToken != null)
            {
                _vm.CancelToken.Cancel();
            }
            await Task.FromResult(_isTest);
        }
    }
}
