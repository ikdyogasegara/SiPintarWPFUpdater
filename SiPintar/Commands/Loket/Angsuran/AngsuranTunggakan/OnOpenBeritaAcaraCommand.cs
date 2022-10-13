using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    public class OnOpenBeritaAcaraCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenBeritaAcaraCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
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
                $"Berita acara angsuran angsuran tunggakan atas nama {_viewModel.SelectedData.Nama} sekarang ?",
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
                    "error",
                    "OK",
                    false,
                    "loket");
            }
            await Task.FromResult(_isTest);
        }
    }
}
