using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly AdministrasiLainViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(AdministrasiLainViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data Tarif Administrasi?",
                    "Data tarif administrasi yang akan dihapus tidak dapat dibatalkan.",
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
