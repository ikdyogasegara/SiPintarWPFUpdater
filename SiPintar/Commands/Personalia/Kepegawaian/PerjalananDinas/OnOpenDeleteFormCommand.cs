using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    $"Batal data perjalanan dinas dengan {System.Environment.NewLine}No. SPPD {_viewModel.SelectedData.NoSppd} ?",
                    $"Data perjalanan dinas yang dihapus tidak dapat dibatalkan.",
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
