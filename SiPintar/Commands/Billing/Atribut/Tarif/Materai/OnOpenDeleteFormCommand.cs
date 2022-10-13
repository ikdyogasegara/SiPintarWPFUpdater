using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Materai
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly MateraiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(MateraiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {

            if (parameter == null)
                return;

            _viewModel.SelectedData = (MasterMeteraiDto)parameter;

            await OpenDialogAsync();
        }

        [ExcludeFromCodeCoverage]
        private async Task OpenDialogAsync()
        {
            if (!_isTest)
                await DialogHost.Show(new DialogGlobalYesCancelView(
                "Hapus Data Tarif Materai?",
                "Data tarif materai yang akan dihapus tidak dapat dibatalkan.",
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
