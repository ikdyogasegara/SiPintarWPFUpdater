using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningLimbah;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnOpenPublishSemuaPasswordCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPublishSemuaPasswordCommand(RekeningLimbahViewModel viewModel, bool isTest = false)
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
