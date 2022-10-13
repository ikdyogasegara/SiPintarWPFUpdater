using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Meteran;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Meteran.MerkMeter
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly MerkMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(MerkMeterViewModel viewModel, bool isTest = false)
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
                    "Hapus Merk Meter",
                    $"Data merk meter yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
