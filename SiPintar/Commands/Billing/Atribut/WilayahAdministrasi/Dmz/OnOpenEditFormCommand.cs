using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Dmz;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Dmz
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly DmzViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(DmzViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeForm = _viewModel.SelectedData.KodeDmz;
            _viewModel.NamaForm = _viewModel.SelectedData.NamaDmz;

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
