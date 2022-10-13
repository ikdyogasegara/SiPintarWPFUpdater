using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Tarif.GolonganLLTT
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly GolonganLlttViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(GolonganLlttViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            await OpenDialogAsync();
        }

        [ExcludeFromCodeCoverage]
        public async Task OpenDialogAsync()
        {
            if (!_isTest)
            {
                await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Tarif Golongan Limbah?", "Data tarif golongan limbah yang akan dihapus tidak dapat dibatalkan.", "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "billing"), "BillingRootDialog");
            }
        }
    }
}
