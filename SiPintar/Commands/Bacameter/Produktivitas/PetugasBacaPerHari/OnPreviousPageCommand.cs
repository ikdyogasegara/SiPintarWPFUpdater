using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.Produktivitas;

namespace SiPintar.Commands.Bacameter.Produktivitas.PetugasBacaPerHari
{
    [ExcludeFromCodeCoverage]
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly PetugasBacaPerHariViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(PetugasBacaPerHariViewModel viewModel, bool isTest = false)
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
