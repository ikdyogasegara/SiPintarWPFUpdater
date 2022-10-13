using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnOpenDeleteDetailFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteDetailFormCommand(SKPegawaiTetapViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedDetailData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaInnerDialog", $"Hapus pegawai {_viewModel.SelectedDetailData.NamaPegawai} ?",
                    $"Data pegawai terpilih yang dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnDeleteDetailFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerDialog",
                    "Hapus Pegawai Tetap",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
