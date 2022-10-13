using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.Kelainan
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly KelainanViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(KelainanViewModel viewModel, bool isTest = false)
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
