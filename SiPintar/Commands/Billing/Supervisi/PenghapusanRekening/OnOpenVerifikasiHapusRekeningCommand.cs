using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PenghapusanRekening;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnOpenVerifikasiHapusRekeningCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenVerifikasiHapusRekeningCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesVerifikasi == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading || _viewModel.RekeningAirHapusSecaraAkuntansiList == null || !_viewModel.RekeningAirHapusSecaraAkuntansiList.Any(c => c.IsSelected == true))
            {
                OpenDialogInvalid();
                return;
            }

            _viewModel.IsLoadingForm = true;
            _viewModel.TglHapus = null;
            _viewModel.Password = null;

            OpenDialog();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new VerifikasiHapusRekeningView(_viewModel), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialogInvalid()
        {
            if (!_isTest)
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Verifikasi Usulan Hapus Secara Akuntansi",
                    "Tidak ada yang dipilih !",
                    "warning",
                    "OK",
                    moduleName: "billing");
        }
    }
}
