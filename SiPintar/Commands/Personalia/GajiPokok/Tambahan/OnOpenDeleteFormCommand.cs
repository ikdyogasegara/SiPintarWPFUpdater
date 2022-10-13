using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(TambahanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsPilih)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "PersonaliaRootDialog",
                    "Warning",
                    "Periode dan Kode Gaji harus dipilih",
                    "warning",
                    "OK",
                    false,
                    "personalia");
                return;
            }

            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus Gaji Tambahan",
                    $"Yakin hapus data gaji tambahan ?",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Gaji Tambahan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
