using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public class OnOpenDeleteConfirmationCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmationCommand(PermohonanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.StatusPermohonanText == "Selesai")
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "HublangRootDialog",
                        "Hapus Tidak diijinkan",
                        $"Permohonan sudah selesai. Hapus Permohonan tidak diijinkan",
                        "warning",
                        "Batal",
                        false,
                        "hublang");
                }
                else if (_viewModel.SelectedData.StatusPermohonanText == "Menunggu Verifikasi")
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "HublangRootDialog",
                        "Hapus Tidak diijinkan",
                        $"Permohonan sudah menjadi usulan koreksi. Hapus Permohonan tidak diijinkan",
                        "warning",
                        "Batal",
                        false,
                        "hublang");
                }
                else
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "HublangRootDialog", $"Hapus Data Permohonan {_viewModel.SelectedData.NamaTipePermohonan}?",
                        $"Data Permohonan {_viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "hublang");

            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "HublangRootDialog",
                    $"Hapus Data Permohonan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "hublang");

            await Task.FromResult(_isTest);
        }
    }
}
