using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Loket;
using SiPintar.Views.Billing.Atribut.Loket.LoketPembatalan;

namespace SiPintar.Commands.Billing.Atribut.Loket.LoketPembatalan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly LoketPembatalanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(LoketPembatalanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.AlasanForm = _viewModel.SelectedData.AlasanBatal;

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
