using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PetugasBaca
{
    public class OnGetFotoDatagridCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnGetFotoDatagridCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            var IdPetugas = _viewModel.SelectedData.IdPetugasBaca;

            if (!_isTest)
                await Task.Delay(2000);

            if (IdPetugas != _viewModel.SelectedData.IdPetugasBaca)
                return;

            _viewModel.IsLoadingFoto = true;
            _viewModel.IsOnGrid = true;

            if (_viewModel.SelectedData.FotoPetugasBaca != null)
                await ((AsyncCommandBase)_viewModel.GetFotoCommand).ExecuteAsync(_viewModel.SelectedData.IdPetugasBaca);


            _viewModel.IsLoadingFoto = false;

            await Task.FromResult(_isTest);
        }
    }
}
