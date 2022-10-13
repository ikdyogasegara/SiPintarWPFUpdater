using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(PaketRabViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data RAB",
                    $"Data RAB yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "perencanaan"
                ), "PerencanaanRootDialog");
        }
    }
}
