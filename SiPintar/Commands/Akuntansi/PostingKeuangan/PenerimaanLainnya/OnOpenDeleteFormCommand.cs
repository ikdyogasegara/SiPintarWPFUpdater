using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdDaftarPenerimaanKas == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Hapus Data Posting Penerimaan Lainnya",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            if (_viewModel.SelectedData?.StatusPostingText == "Sudah Posting")
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Hapus Data Posting Penerimaan Lainnya",
                    "Maaf data tidak dapat dihapus, karena sudah mengalami Proses POSTING",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Posting Penerimaan Lainnya?",
                $"Data yang akan dihapus tidak dapat dibatalkan.",
                "confirmation",
                _viewModel.OnSubmitDeleteFormCommand,
                "Ya",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
