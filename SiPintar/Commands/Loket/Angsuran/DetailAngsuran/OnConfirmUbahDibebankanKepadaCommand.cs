using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnConfirmUbahDibebankanKepadaCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnConfirmUbahDibebankanKepadaCommand(DetailAngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.SelectedData?.NoAngsuran != null)
            {
                DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");

                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "LoketRootDialog",
                $"Konfirmasi Ubah Penanggung Beban",
                $"Ganti penanggung beban angsuran {_viewModel.NamaBefore} - {_viewModel.NosambBefore} menjadi {_viewModel.PelangganSelected.Nama} - {_viewModel.PelangganSelected.NoSamb} sekarang ?",
                "warning",
                _viewModel.OnSubmitUbahPenanggungBebanCommand,
                "Ganti",
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
                    "Konfirmasi Ubah Penanggung Beban",
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
