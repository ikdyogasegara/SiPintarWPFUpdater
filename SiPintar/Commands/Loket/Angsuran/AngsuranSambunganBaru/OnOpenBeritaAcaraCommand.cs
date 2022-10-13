using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    public class OnOpenBeritaAcaraCommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenBeritaAcaraCommand(AngsuranSambunganBaruViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.NoAngsuran != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "LoketRootDialog",
                $"Konfirmasi Berita Acara Angsuran",
                $"Berita acara angsuran sambungan baru atas nama {_viewModel.SelectedData.Nama} sekarang ?",
                "success",
                _viewModel.OnSubmitBeritaAcaraCommand,
                "Berita Acara",
                "Batal",
                false,
                false,
                "loket");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "LoketRootDialog",
                    "Berita Acara Angsuran",
                    "Data belum dipilih",
                    "warning",
                    "OK",
                    false,
                    "loket");
            }
            await Task.FromResult(_isTest);
        }
    }
}
