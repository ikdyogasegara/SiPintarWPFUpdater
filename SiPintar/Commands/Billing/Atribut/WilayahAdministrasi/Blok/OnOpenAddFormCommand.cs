using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Blok;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Blok
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly BlokViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(BlokViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.KodeBlokForm = null;
            _viewModel.NamaBlokForm = null;
            _viewModel.WilayahForm = null;
            _viewModel.AreaForm = null;
            _viewModel.RayonForm = null;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

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
