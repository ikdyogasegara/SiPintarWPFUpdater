using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Pengaturan;

namespace SiPintar.Commands.Akuntansi.Pengaturan.SettingTtd
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly SettingTtdViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isSelected;

        public OnOpenDeleteFormCommand(SettingTtdViewModel viewModel, bool isTest = false, bool isSelected = false)
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
                    $"Hapus Setting TTD?",
                    $"Data Setting TTD yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Setting TTD",
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
