using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnOpenUploadUlangCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenUploadUlangCommand(RekeningLimbahViewModel viewModel, bool isTest = false)
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
                if (_viewModel.SelectedData.FlagPublish == false)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Belum publish", "Silakan publish tagihan terlebih dahulu untuk melakukan upload ulang.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                if (_viewModel.SelectedData.FlagUpload == true)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Sudah Diupload", "Tagihan rekening sudah diupload sebelumnya.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Upload Ulang Rekening Limbah?",
                    $@"Apakah Anda yakin melakukan upload ulang '{_viewModel.SelectedData.Nama} - {_viewModel.PeriodeFilter.NamaPeriode}'",
                    "warning",
                    _viewModel.OnSubmitUploadUlangCommand,
                    "Upload ulang",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }
    }
}
