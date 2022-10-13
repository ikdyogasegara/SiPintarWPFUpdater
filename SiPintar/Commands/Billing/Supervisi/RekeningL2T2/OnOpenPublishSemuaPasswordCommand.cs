using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningL2T2;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnOpenPublishSemuaPasswordCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPublishSemuaPasswordCommand(RekeningL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);

                _ = DialogHost.Show(new KonfirmasiVerifikasiView(_viewModel), "BillingRootDialog");
            }
        }
    }
}
