using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views.Akuntansi.Jurnal.Umum;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnOpenTransaksiDeleteFormCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isTrue;

        public OnOpenTransaksiDeleteFormCommand(UmumViewModel viewModel, bool isTest = false, bool isTrue = true)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _isTrue = isTrue;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_isTrue)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Hapus Data Transaksi?",
                    $"Data 50.02.10 yang dihapus tidak dapat dibatalkan.",
                    "warning",
                    null,
                    "Ya",
                    "Batal",
                    false,
                    false,
                    "akuntansi",
                    _viewModel.OnOpenAddFormCommand);
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Hapus Data Transaksi?",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");
            }
            await Task.FromResult(_isTest);
        }
    }
}
