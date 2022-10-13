using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranNonAir
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly AngsuranNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(AngsuranNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
