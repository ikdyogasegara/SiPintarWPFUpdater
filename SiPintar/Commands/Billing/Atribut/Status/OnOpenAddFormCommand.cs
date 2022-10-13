using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Status;

namespace SiPintar.Commands.Billing.Atribut.Status
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly StatusViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(StatusViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.NamaForm = null;
            _viewModel.IncludeRekeningAirForm = false;
            _viewModel.IncludeRekeningLimbahForm = false;
            _viewModel.IncludeRekeningLLTTForm = false;
            _viewModel.TanpaBiayaAirForm = false;

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
