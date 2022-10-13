using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Rayon
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly RayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(RayonViewModel viewModel, bool isTest = false)
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

            ShowDialog(section);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string section)
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Hapus data {section}",
                    $"Data {section} yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "bacameter"
                ), "BacameterRootDialog");
        }
    }
}
