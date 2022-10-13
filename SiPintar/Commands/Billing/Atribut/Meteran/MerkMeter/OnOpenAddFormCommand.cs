using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Meteran;
using SiPintar.Views.Billing.Atribut.Meteran.MerkMeter;

namespace SiPintar.Commands.Billing.Atribut.Meteran.MerkMeter
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly MerkMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(MerkMeterViewModel viewModel, bool isTest = false)
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
