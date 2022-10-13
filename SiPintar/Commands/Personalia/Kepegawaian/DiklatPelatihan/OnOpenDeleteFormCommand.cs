using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(DiklatPelatihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    $"Hapus data diklat & pelatihan dengan {System.Environment.NewLine}No. {_viewModel.SelectedData.NoSertifikat} ?",
                    $"Data diklat & pelatihan yang dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Diklat & Pelatihan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
