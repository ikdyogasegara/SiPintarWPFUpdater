using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(InteraksiJenisPersediaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.SelectedData?.IdInPersediaan != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Interaksi Persediaan?",
                $"Data Jenis Interaksi Persediaan yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Data Interaksi Persediaan",
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
