using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Loket;
using SiPintar.Views.Billing.Atribut.Loket.LoketPembatalan;

namespace SiPintar.Commands.Billing.Atribut.Loket.LoketPembatalan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly LoketPembatalanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(LoketPembatalanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.AlasanForm = null;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {

            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
