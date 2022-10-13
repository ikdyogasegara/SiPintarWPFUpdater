using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(MasterTunjanganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus data master tunjangan \"{_viewModel.SelectedData.NamaJenisTunjangan}\"",
                    $"Data Master Tunjangan yang dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Data Master Tunjangan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
