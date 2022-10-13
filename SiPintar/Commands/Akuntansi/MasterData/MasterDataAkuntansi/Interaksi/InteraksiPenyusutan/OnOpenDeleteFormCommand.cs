using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(InteraksiPenyusutanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdInPenyusutan != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Penyusutan",
                $"Data Jenis Penyusutan yang dihapus tidak dapat dibatalkan.",
                "warning",
                _viewModel.OnSubmitDeleteFormCommand,
                "Proses",
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
                    "Hapus Data Penyusutan",
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
