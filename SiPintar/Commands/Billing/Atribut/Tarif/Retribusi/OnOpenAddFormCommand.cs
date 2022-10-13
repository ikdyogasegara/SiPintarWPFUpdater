using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.RetribusiLain;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Retribusi
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly RetribusiViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(RetribusiViewModel viewModel, bool isTest = false)
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
            _viewModel.KodeRetribusiForm = null;
            _viewModel.NamaRetribusiForm = null;
            _viewModel.BiayaRetribusiForm = 0;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
