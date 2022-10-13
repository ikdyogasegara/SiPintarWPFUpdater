using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.RetribusiLain;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Retribusi
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly RetribusiViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(RetribusiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeRetribusiForm = _viewModel.SelectedData.KodeRetribusiLain;
            _viewModel.NamaRetribusiForm = _viewModel.SelectedData.NamaRetribusiLain;
            _viewModel.BiayaRetribusiForm = _viewModel.SelectedData.Retribusi;

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
