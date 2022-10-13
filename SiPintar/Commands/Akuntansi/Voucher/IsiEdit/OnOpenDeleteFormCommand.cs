using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Commands.Akuntansi.Voucher.IsiEdit
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly IsiEditViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isSelected;

        public OnOpenDeleteFormCommand(IsiEditViewModel viewModel, bool isTest = false, bool isSelected = true)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _isSelected = isSelected;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            if (_isSelected)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Hapus Data Voucher?",
                    $"Data 001/VC/XII/2021 yang dihapus tidak dapat dibatalkan.",
                    "warning",
                    null,
                    "Ya",
                    "Batal",
                    false,
                    false,
                    "akuntansi");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Hapus Data Voucher",
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
