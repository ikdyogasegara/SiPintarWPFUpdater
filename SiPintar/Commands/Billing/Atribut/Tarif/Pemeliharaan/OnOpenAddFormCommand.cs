using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.PemeliharaanLain;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Pemeliharaan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PemeliharaanViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(PemeliharaanViewModel viewModel, bool isTest = false)
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
            _viewModel.KodePemeliharaanForm = null;
            _viewModel.NamaPemeliharaanForm = null;
            _viewModel.BiayaPemeliharaanForm = 0;

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
