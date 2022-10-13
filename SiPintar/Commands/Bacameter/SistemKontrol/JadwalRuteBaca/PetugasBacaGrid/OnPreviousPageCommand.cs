using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol.JadwalRuteBaca;

namespace SiPintar.Commands.Bacameter.SistemKontrol.JadwalRuteBaca.PetugasBacaGrid
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly PetugasBacaGridViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(PetugasBacaGridViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                LoadData();
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void LoadData()
        {
            if (!_isTest)
                _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
