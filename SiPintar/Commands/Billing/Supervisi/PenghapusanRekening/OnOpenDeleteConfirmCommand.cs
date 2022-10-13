using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesBatalkan == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading || _viewModel.RekeningAirHapusSecaraAkuntansiList == null ||
                !_viewModel.RekeningAirHapusSecaraAkuntansiList.Any(c => c.IsSelected == true))
            {
                ShowDialogInvalid();
                return;
            }

            ShowDialog(_viewModel);
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(PenghapusanRekeningViewModel model)
        {
            if (!_isTest)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Konfirmasi Pembatalan?",
                    $@"Anda akan membatalkan data yang dipilih dari daftar usulan penghapusan rekening??",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialogInvalid()
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Batalkan Usulan Hapus Secara Akuntansi",
                    "Tidak ada yang dipilih !",
                    "warning",
                    "OK",
                    moduleName: "billing");
            }
        }
    }
}
