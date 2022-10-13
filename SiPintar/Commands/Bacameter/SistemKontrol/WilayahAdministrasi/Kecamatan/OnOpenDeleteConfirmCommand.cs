using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Kecamatan
{
    public class OnOpenDeleteConfirmCommand : AsyncCommandBase
    {
        private readonly KecamatanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmCommand(KecamatanViewModel viewModel, bool isTest = false)
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
