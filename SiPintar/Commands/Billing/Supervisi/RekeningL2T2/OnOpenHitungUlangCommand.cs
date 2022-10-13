using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnOpenHitungUlangCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenHitungUlangCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PeriodeFilter == null || _viewModel.SelectedData == null)
                return;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.FlagPublish == true)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Telah publish", "Silakan unpublish tagihan terlebih dahulu sebelum hitung ulang.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hitung Ulang Rekening L2T2?",
                    $@"Apakah Anda yakin melakukan hitung ulang tagihan milik '{_viewModel.SelectedData.Nama} - {_viewModel.PeriodeFilter.NamaPeriode}'",
                    "warning",
                    _viewModel.OnSubmitHitungUlangCommand,
                    "Hitung Ulang",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }
    }
}
