using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnOpenHapusRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenHapusRekeningCommand(RekeningLimbahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            var Information = $"{_viewModel.SelectedData.Nama} - {_viewModel.PeriodeFilter.NamaPeriode}";

            ShowDialog(Information);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string Information)
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.FlagPublish == true)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Telah publish", "Silakan unpublish tagihan terlebih dahulu untuk menghapus data.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Periode DRD?",
                    $@"Apakah Anda yakin ingin menghapus data '{Information}'?
Seluruh data terkait (piutang & histori) rekening ini akan hilang dan tidak dapat dibatalkan",
                    "warning",
                    _viewModel.OnSubmitHapusRekeningCommand,
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
