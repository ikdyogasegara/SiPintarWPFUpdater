using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Kelurahan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Kelurahan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KelurahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KelurahanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingCabang || _viewModel.IsLoadingKecamatan || _viewModel.IsLoadingKelurahan)
                return;

            var section = parameter as string;
            _viewModel.Section = section;

            if (section == "kecamatan" && _viewModel.SelectedCabang == null)
            {
                ShowError("Terjadi Kesalahan", "Silakan pilih Cabang terlebih dahulu", "error");
                return;
            }
            else if (section == "kelurahan" && _viewModel.SelectedKecamatan == null)
            {
                ShowError("Terjadi Kesalahan", "Silakan pilih Kecamatan terlebih dahulu", "error");
                return;
            }

            _viewModel.IsEdit = false;
            _viewModel.KodeForm = null;
            _viewModel.NamaForm = null;
            _viewModel.JumlahJiwaForm = null;

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
        private void ShowError(string title, string message, string type)
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(title, message, type), "BillingRootDialog");
        }
    }
}
