using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(KelompokKodePerkiraan3ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdPerkiraan3 != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Kode Perkiraan (XX.YY.ZZ)?",
                $"Kode perkiraan 3 yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Data Kode Perkiraan (XX.YY.ZZ)",
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
