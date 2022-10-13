using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(GantiMeterNonRutinViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.Kategori != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "DistribusiRootDialog",
                $"Konfirmasi Hapus Atribut Kelainan Ganti Meter Rutin",
                $"Apakah Anda ingin menghapus data atribut kelainan ganti meter {_viewModel.SelectedData.JenisGantiMeter} ?",
                "confirmation",
                _viewModel.OnSubmitDeleteCommand,
                "Ya",
                "Batal",
                false,
                false,
                "distribusi");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "DistribusiRootDialog",
                    "Hapus Atribut Kelainan Ganti Meter Non Rutin",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "distribusi");
            }
            await Task.FromResult(_isTest);
        }
    }
}
