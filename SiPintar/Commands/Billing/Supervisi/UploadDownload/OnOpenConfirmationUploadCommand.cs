using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.UploadDownload
{
    public class OnOpenConfirmationUploadCommand : AsyncCommandBase
    {
        private readonly UploadDownloadViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenConfirmationUploadCommand(UploadDownloadViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            int UploadCount = 0;
            UploadCount += _viewModel.TagihanAir ? 1 : 0;
            UploadCount += _viewModel.TagihanLimbah ? 1 : 0;
            UploadCount += _viewModel.TagihanLltt ? 1 : 0;
            UploadCount += _viewModel.SinkronParameter ? 1 : 0;

            ShowDialog(UploadCount);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(int UploadCount)
        {
            if (!_isTest)
            {
                if (UploadCount > 0)
                    _ = DialogHost.Show(new DialogGlobalYesCancelView(
                        "Konfirmasi Upload",
                        "Anda akan mengupload data Tagihan Air dan Sinkronasi Parameter",
                        "confirmation",
                        _viewModel.OnConfirmUploadCommand,
                        "Upload",
                        "Batal",
                        false,
                        false,
                        "billing"), "BillingRootDialog");
                else
                    _ = DialogHost.Show(new DialogGlobalView("Upload Data", "Tidak ada data yang dipilih", "warning", "Ok", false, "billing"), "BillingRootDialog");
            }
        }
    }
}
