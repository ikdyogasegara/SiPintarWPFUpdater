using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Kelurahan;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Kelurahan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KelurahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KelurahanViewModel viewModel, bool isTest = false)
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

            if (section == "cabang" && _viewModel.SelectedCabang == null)
                return;
            else if (section == "kecamatan" && _viewModel.SelectedKecamatan == null)
                return;
            else if (section == "kelurahan" && _viewModel.SelectedKelurahan == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeForm = GetKode(section);
            _viewModel.NamaForm = GetNama(section);
            _viewModel.JumlahJiwaForm = GetJumlahJiwa(section);

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

        private string GetKode(string section)
        {
            string result = string.Empty;
            switch (section)
            {
                case "cabang":
                    result = _viewModel.SelectedCabang.KodeCabang;
                    break;
                case "kecamatan":
                    result = _viewModel.SelectedKecamatan.KodeKecamatan;
                    break;
                case "kelurahan":
                    result = _viewModel.SelectedKelurahan.KodeKelurahan;
                    break;
                default:
                    break;
            }

            return result;
        }

        private string GetNama(string section)
        {
            string result = string.Empty;
            switch (section)
            {
                case "cabang":
                    result = _viewModel.SelectedCabang.NamaCabang;
                    break;
                case "kecamatan":
                    result = _viewModel.SelectedKecamatan.NamaKecamatan;
                    break;
                case "kelurahan":
                    result = _viewModel.SelectedKelurahan.NamaKelurahan;
                    break;
                default:
                    break;
            }

            return result;
        }

        private string GetJumlahJiwa(string section)
        {
            string result = null;
            switch (section)
            {
                case "cabang":
                    break;
                case "kecamatan":
                    break;
                case "kelurahan":
                    result = _viewModel.SelectedKelurahan.JumlahJiwa.ToString();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
