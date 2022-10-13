using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(InteraksiLayananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.SelectedData?.IdInPelayananAir != null || _viewModel.SelectedData?.IdInPelayananNonAir != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Interaksi Layanan & Kd.Perkiraan(Air / Non)",
                $"Data Interaksi Layanan & Kd. Perkiraan (Air/Non) yang dihapus tidak dapat dibatalkan.",
                "confirmation",
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
                    "Hapus Data Interaksi Layanan & Kd.Perkiraan(Air / Non)",
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
