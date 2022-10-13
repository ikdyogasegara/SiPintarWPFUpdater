using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnConfirmDeleteFormCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly bool _isTest;

        public OnConfirmDeleteFormCommand(TunjanganBulananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var message = "";
            if (_viewModel.DeleteMode1)
                message = $"Hapus Tunjangan {_viewModel.SelectedData.NamaJenisTunjangan} untuk yang terpilih";
            else if (_viewModel.DeleteMode2)
                message = $"Hapus Semua Tunjangan {_viewModel.SelectedData.NamaJenisTunjangan} pada periode {_viewModel.FilterPeriode.NamaPeriode}";
            else if (_viewModel.DeleteMode3)
                message = $"Hapus Semua Tunjangan di kode gaji {_viewModel.FilterKodeGaji.KodeGaji} pada periode {_viewModel.FilterPeriode.NamaPeriode}";

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", message,
                    "Data tunjangan bulanan yang dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
