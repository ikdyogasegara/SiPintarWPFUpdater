using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.AreaWilayah;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.AreaWilayah
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly AreaWilayahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(AreaWilayahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingWilayah || _viewModel.IsLoadingArea || _viewModel.IsLoadingRayon)
                return;

            var section = parameter as string;
            _viewModel.Section = section;

            if (section == "area" && _viewModel.SelectedWilayah == null)
            {
                ShowWarningWilayah();
                return;
            }
            else if (section == "rayon" && _viewModel.SelectedArea == null)
            {
                ShowWarningArea();
                return;
            }

            _viewModel.IsEdit = false;
            _viewModel.KodeForm = null;
            _viewModel.NamaForm = null;

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

        [ExcludeFromCodeCoverage]
        private void ShowWarningWilayah()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", "Silakan pilih Wilayah terlebih dahulu", "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowWarningArea()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", "Silakan pilih Area terlebih dahulu", "error"), "BillingRootDialog");
        }
    }
}
