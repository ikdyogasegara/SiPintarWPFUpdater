using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.UploadDownload
{
    public class OnOpenConfirmationDownloadCommand : AsyncCommandBase
    {
        private readonly UploadDownloadViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenConfirmationDownloadCommand(UploadDownloadViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Konfirmasi Download Transaksi",
                    "Anda akan mendownload transaksi pembayaran dan pembatalan.",
                    "confirmation",
                    _viewModel.OnConfirmDownloadCommand,
                    "Download",
                    "Batal", false, false, "billing"), "BillingRootDialog");
        }
    }
}
