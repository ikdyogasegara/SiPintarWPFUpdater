using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnOpenPublishSemuaConfirmationCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPublishSemuaConfirmationCommand(RekeningLimbahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PeriodeFilter == null || _viewModel.RekeningList.Count <= 0)
                return;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Publish Semua Rekening Limbah?",
                    $@"Anda akan mempublish semua 'rekening limbah periode {_viewModel.PeriodeFilter.NamaPeriode}'",
                    "warning",
                    _viewModel.OnOpenPublishSemuaPasswordCommand,
                    "Publish Semua",
                    "Batal",
                    false,
                    false,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
