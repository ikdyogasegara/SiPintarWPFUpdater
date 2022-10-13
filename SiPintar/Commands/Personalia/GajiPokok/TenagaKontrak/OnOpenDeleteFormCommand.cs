using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaKontrak
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TenagaKontrakViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(TenagaKontrakViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus Gaji Tenaga Kontrak",
                    $"Yakin hapus data gaji pegawai kontrak ?",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Gaji Tenaga Kontrak",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
