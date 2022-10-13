using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(SKCalonPegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.Verifikasi == true)
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PersonaliaRootDialog",
                        "Hapus tidak diizinkan",
                        "Data sudah diverifikasi, Jika ingin menghapus/membatalkan harap hubungi admin",
                        "error",
                        "Batal",
                        false,
                        "Personalia");
                else
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false,
                        "PersonaliaRootDialog",
                        $"Batal data SK Calon Pegawai dengan {System.Environment.NewLine}No. SK {_viewModel.SelectedData.NoSk} ?",
                        $"Data mutasi yang dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus SK Calon Pegawai",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
