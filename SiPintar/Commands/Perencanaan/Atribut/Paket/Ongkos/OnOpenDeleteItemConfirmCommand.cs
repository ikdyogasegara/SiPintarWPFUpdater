using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeleteItemConfirmCommand : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteItemConfirmCommand(PaketOngkosViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingPaket || _viewModel.IsLoadingRincian || _viewModel.SelectedRincian == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Hapus data ongkos?",
                    $"Data ongkos yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeleteItemCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "perencanaan"
                ), "PerencanaanRootDialog");
        }
    }
}
