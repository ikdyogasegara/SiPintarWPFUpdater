using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(KelompokKodePerkiraan2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdPerkiraan2 != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Kode Perkiraan (XX.YY)?",
                $"Kode perkiraan 2 yang dihapus tidak dapat dibatalkan.",
                "confirmation",
                _viewModel.OnSubmitDeleteFormCommand,
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
                    "Hapus Data Kode Perkiraan (XX.YY)",
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
