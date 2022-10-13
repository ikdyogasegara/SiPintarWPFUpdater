using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PersentaseTarifPajakViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.SelectedData?.Id != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Hapus Data IPengelompokan Data Aktiva",
                $"Data Pengelompokan Data Aktiva yang dihapus tidak dapat dibatalkan.",
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
                    "Hapus Data Pengelompokan Data Aktiva",
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
