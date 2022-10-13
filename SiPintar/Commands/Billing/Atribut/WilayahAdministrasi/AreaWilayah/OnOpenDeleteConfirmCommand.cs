using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.AreaWilayah
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly AreaWilayahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(AreaWilayahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingWilayah || _viewModel.IsLoadingArea || _viewModel.IsLoadingRayon)
                return;

            var section = parameter as string;
            _viewModel.Section = section;

            if (section == "wilayah" && _viewModel.SelectedWilayah == null)
                return;
            else if (section == "area" && _viewModel.SelectedArea == null)
                return;
            else if (section == "rayon" && _viewModel.SelectedRayon == null)
                return;

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "BillingRootDialog", $"Hapus Data {section} ?",
                $"Data {section} yang akan dihapus tidak dapat dibatalkan.",
                "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "billing");

            await Task.FromResult(true);
        }
    }
}
