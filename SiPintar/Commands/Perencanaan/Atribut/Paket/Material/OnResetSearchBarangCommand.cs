using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnResetSearchBarangCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetSearchBarangCommand(PaketMaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTriggerSearch = false;
            _viewModel.NamaBarangForm = null;

            _viewModel.OnSearchBarangCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
