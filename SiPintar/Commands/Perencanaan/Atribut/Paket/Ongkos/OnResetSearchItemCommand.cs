using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    public class OnResetSearchItemCommand : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetSearchItemCommand(PaketOngkosViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTriggerSearch = false;
            _viewModel.NamaItemForm = null;

            _viewModel.OnSearchItemCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
