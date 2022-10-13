using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.AdministrasiLain;

namespace SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly AdministrasiLainViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(AdministrasiLainViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeAdministrasiForm = _viewModel.SelectedData.KodeAdministrasiLain;
            _viewModel.NamaAdministrasiForm = _viewModel.SelectedData.NamaAdministrasiLain;
            _viewModel.BiayaAdministrasiForm = _viewModel.SelectedData.Administrasi;

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
