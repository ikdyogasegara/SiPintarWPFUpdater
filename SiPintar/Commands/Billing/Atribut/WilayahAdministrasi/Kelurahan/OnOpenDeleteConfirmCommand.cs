using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Kelurahan
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly KelurahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(KelurahanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingCabang || _viewModel.IsLoadingKecamatan || _viewModel.IsLoadingKelurahan)
                return;

            var section = parameter as string;

            _viewModel.Section = section;

            if (section == "cabang" && _viewModel.SelectedCabang == null)
                return;
            else if (section == "kecamatan" && _viewModel.SelectedKecamatan == null)
                return;
            else if (section == "kelurahan" && _viewModel.SelectedKelurahan == null)
                return;

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "BillingRootDialog", $"Hapus Data {section} ?",
                $"Data {section} yang akan dihapus tidak dapat dibatalkan.",
                "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "billing");

            await Task.FromResult(true);
        }
    }
}
