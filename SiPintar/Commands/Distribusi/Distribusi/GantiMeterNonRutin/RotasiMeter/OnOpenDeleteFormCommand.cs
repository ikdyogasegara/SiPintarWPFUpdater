using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(RotasiMeterNonRutinViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdPelangganAir != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    _isTest,
                    false,
                    "DistribusiRootDialog",
                    $"Konfirmasi Hapus Rotasi Meter",
                    $"Apakah anda ingin menghapus data rotasi meter {_viewModel.SelectedData.Nama} ?",
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
                    "Hapus Rotasi Meter",
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
