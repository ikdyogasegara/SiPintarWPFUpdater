using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenDownloadBacameterCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDownloadBacameterCommand(RekeningAirViewModel viewModel, bool isTest = false)
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
                if (_viewModel.AksesDownloadBacameter == false)
                {
                    DialogHelper.ShowInvalidAkses(_isTest);
                    return;
                }
                #endregion

                if (_viewModel.SelectedDataPeriode.IdPeriode == null) return;

                await DialogHost.Show(new DialogGlobalYesCancelView("Download Data Bacameter", "Anda akan mendownload & sinkronisasi data bacameter - " + _viewModel.SelectedDataPeriode.NamaPeriode + " ke rekening air.", "confirmation",
                    _viewModel.OnOpenVerifikasiBacameterCommand, "Download Bacameter", "Batal"), "BillingRootDialog");
            }
            await Task.FromResult(true);
        }
    }
}
