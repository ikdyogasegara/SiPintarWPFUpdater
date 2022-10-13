using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnOpenDeleteConfirmationCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmationCommand(DataPembacaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            var SelectedDate = _viewModel.SelectedData.NamaPeriode;

            ShowDialog(SelectedDate);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string SelectedDate)
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data Pembacaan?",
                    $@"Daftar awal bulan: {SelectedDate} akan dihapus?",
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
