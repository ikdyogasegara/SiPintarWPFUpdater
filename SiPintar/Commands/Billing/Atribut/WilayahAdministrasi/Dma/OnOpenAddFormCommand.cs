using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Dma;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Dma
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DmaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DmaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.KodeForm = null;
            _viewModel.NamaForm = null;

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
