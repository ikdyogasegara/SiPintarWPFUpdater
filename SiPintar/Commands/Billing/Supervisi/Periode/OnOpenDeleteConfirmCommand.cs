using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesHapus == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            var selectedDate = _viewModel.SelectedData.NamaPeriode;

            ShowDialog(selectedDate);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string selectedDate)
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobal3MessagesViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Hapus Periode DRD",
                    $"Apakah Anda yakin ingin menghapus data DRD periode {selectedDate}?",
                    $"Seluruh data terkait (piutang & histori) bulan {selectedDate} akan hilang dan tidak dapat dibatalkan.",
                    null,
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    moduleName: "billing");
            }
        }
    }
}
