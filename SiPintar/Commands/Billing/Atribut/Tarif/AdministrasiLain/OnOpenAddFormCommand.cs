using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.AdministrasiLain;

namespace SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly AdministrasiLainViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(AdministrasiLainViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;

            _viewModel.IsLoadingForm = true;

            _viewModel.KodeAdministrasiForm = null;
            _viewModel.NamaAdministrasiForm = null;
            _viewModel.BiayaAdministrasiForm = 0;

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
