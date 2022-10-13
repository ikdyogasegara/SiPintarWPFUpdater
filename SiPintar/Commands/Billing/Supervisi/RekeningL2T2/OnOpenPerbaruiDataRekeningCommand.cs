using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningL2T2;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnOpenPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPerbaruiDataRekeningCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;
            _viewModel.PeriodePerbaruiForm = _viewModel.PeriodeList?.FirstOrDefault(i => i.IdPeriode == _viewModel.SelectedData.IdPeriode);

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.FlagPublish == true)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Telah publish", "Silakan unpublish tagihan terlebih dahulu untuk memperbarui data.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                _ = DialogHost.Show(new PerbaruiDataRekeningView(_viewModel), "BillingRootDialog");
            }
        }
    }
}
