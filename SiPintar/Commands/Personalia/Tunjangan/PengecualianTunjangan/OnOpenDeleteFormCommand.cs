using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PengecualianTunjanganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus pegawai \"{_viewModel.SelectedData.NamaPegawai}\" dari daftar pengecualian tunjangan",
                    $"Data Pengecualian Tunjangan yang dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Data Pengecualian Tunjangan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
