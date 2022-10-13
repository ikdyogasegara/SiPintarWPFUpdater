using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.Produktivitas;

namespace SiPintar.Commands.Bacameter.Produktivitas.ProgressPembacaan
{
    [ExcludeFromCodeCoverage]
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly ProgressPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(ProgressPembacaanViewModel viewModel, bool isTest = false)
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
