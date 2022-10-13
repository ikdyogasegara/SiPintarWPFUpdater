using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Views.Bacameter.SistemKontrol.WilayahAdministrasi.Rayon;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Rayon
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly RayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(RayonViewModel viewModel, bool isTest = false)
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

            if (section == "wilayah" && _viewModel.SelectedWilayah == null)
                return;
            else if (section == "area" && _viewModel.SelectedArea == null)
                return;
            else if (section == "rayon" && _viewModel.SelectedRayon == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeForm = GetKode(section);
            _viewModel.NamaForm = GetNama(section);

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BacameterRootDialog");
        }

        private string GetKode(string section)
        {
            string result = string.Empty;
            switch (section)
            {
                case "wilayah":
                    result = _viewModel.SelectedWilayah.KodeWilayah;
                    break;
                case "area":
                    result = _viewModel.SelectedArea.KodeArea;
                    break;
                case "rayon":
                    result = _viewModel.SelectedRayon.KodeRayon;
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
                case "wilayah":
                    result = _viewModel.SelectedWilayah.NamaWilayah;
                    break;
                case "area":
                    result = _viewModel.SelectedArea.NamaArea;
                    break;
                case "rayon":
                    result = _viewModel.SelectedRayon.NamaRayon;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
