using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PersentasePenyusutanAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.SelectedData != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data Persentase Penyusutan Aktiva",
                $"Data Persentase Penyusutan Aktiva yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Data Persentase Penyusutan Aktiva",
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
