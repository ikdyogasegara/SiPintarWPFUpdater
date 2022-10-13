using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnOpenDeleteConfirmationCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmationCommand(UsulanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.StatusVerifikasiText.Left(9) == "(Selesai)")
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "HublangRootDialog",
                        "Hapus Tidak diijinkan",
                        $"Usulan sudah selesai. Hapus Usulan tidak diijinkan",
                        "warning",
                        "Batal",
                        false,
                        "hublang");
                }
                else
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "HublangRootDialog", $"Hapus Data Usulan {_viewModel.SelectedData.NamaTipePermohonan}?",
                        $"Data Usulan {_viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteCommand, "Hapus", "Batal", false, false, "hublang");

            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "HublangRootDialog",
                    $"Hapus Data Usulan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "hublang");

            await Task.FromResult(_isTest);
        }
    }
}
