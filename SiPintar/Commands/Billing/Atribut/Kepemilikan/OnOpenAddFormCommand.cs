using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Kepemilikan;

namespace SiPintar.Commands.Billing.Atribut.Kepemilikan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KepemilikanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KepemilikanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.KodeKepemilikanForm = null;
            _viewModel.NamaKepemilikanForm = null;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
