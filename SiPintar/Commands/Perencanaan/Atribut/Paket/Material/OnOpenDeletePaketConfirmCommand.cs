using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeletePaketConfirmCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeletePaketConfirmCommand(PaketMaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingPaket || _viewModel.IsLoadingRincian || _viewModel.SelectedPaket == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Hapus data paket?",
                    $"Data paket material yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeletePaketCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "perencanaan"
                ), "PerencanaanRootDialog");
        }
    }
}
