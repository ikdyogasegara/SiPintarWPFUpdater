using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isTrue;

        public OnOpenDeleteFormCommand(DataAktivaViewModel viewModel, bool isTest = false, bool isTrue = true)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _isTrue = isTrue;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            if (_isTrue)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Hapus Data Aktiva?",
                    $"Data 31.02.20 yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Data Aktiva",
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
