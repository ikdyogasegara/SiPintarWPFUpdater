using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnOpenChangeStatusConfirmCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenChangeStatusConfirmCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            var section = parameter as string;
            _viewModel.StatusSection = section;

            var selectedDate = _viewModel.SelectedData.NamaPeriode;

            ShowDialog(selectedDate, section);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string selectedDate, string section)
        {
            var headerInfo = section == "open" ? "Aktifkan Periode" : "Tutup Periode";
            var sectionInfo = section == "open" ? "mengaktifkan kembali" : "menutup";

            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    headerInfo,
                    $@"Apakah Anda yakin ingin {sectionInfo} data DRD periode {selectedDate}?",
                    "warning",
                    _viewModel.OnSubmitChangeStatusCommand,
                    "Proses",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
