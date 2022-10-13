using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningL2T2;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnOpenKoreksiRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKoreksiRekeningCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.BiayaKoreksiForm = _viewModel.SelectedData.Biaya.ToString();
            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.FlagPublish == true)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tagihan Telah publish", "Silakan unpublish tagihan terlebih dahulu untuk melakukan koreksi rekening L2T2.", "error", "Oke", false, "billing"), "BillingRootDialog");
                    return;
                }

                _ = DialogHost.Show(new KoreksiRekeningL2T2View(_viewModel), "BillingRootDialog");
            }
        }
    }
}
