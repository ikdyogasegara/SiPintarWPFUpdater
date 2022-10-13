using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Golongan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(GolonganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            await OpenDialogAsync();

            _viewModel.IsFromDetail = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public async Task OpenDialogAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.IsFromDetail)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Tarif Golongan?", "Data tarif golongan yang akan dihapus tidak dapat dibatalkan.", "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "billing"), "BillingRootDialog");
            }
        }
    }
}
