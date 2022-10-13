using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenHapusRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenHapusRekeningCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest)
            {
                #region akses
                if (_viewModel.AksesHapus == false)
                {
                    DialogHelper.ShowInvalidAkses(_isTest);
                    return;
                }
                #endregion

                if (_viewModel.SelectedData is { FlagPublishWpf: true })
                {
                    OpenDialogWarning();
                }
                else
                {
                    await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Rekening Air Pelanggan?",
                        "Apakah anda akan Menghapus data rekening " + _viewModel.SelectedData.Nama + " Periode " + _viewModel.SelectedData.NamaPeriode + "?", "warning", _viewModel.OnSubmitDeleteRekeningCommand, "Hapus", "Batal", false, false, "billing"), "BillingRootDialog");
                }
            }

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialogWarning()
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Hapus Rekening",
                    $"Rekening publish tidak dapat di hapus !",
                    "warning",
                    moduleName: "billing");
            }
        }
    }
}
