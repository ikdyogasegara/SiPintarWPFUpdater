using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PenghapusanRekening;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnOpenConfirmVerifikasiCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenConfirmVerifikasiCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (!_isTest) _ = await DialogHost.Show(new KonfirmasiVerifikasiView(_viewModel), "BillingRootDialog");
        }
    }
}
