using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Produktivitas;

namespace SiPintar.Commands.Billing.Produktivitas.PetugasBacaPerHari
{
    [ExcludeFromCodeCoverage]
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly PetugasBacaPerHariViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(PetugasBacaPerHariViewModel viewModel, bool isTest = false)
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
