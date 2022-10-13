using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
