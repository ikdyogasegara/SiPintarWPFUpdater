using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Kolektif
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly KolektifViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(KolektifViewModel viewModel, bool isTest = false)
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
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data Kolektif Penagihan",
                    "Data kolektif penagihan yang akan dihapus tidak dapat dibatalkan.",
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
}
