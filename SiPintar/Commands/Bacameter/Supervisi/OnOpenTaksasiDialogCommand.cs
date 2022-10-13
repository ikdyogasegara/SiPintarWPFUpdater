using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenTaksasiDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenTaksasiDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            ShowDialog(_viewModel.SelectedData);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(RekeningAirDto SelectedData)
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Taksasi Pemakaian Otomatis",
                    $@"Apakah Anda yakin ingin melakukan flag taksasi milik '{SelectedData.Nama}' pada periode '{SelectedData.NamaPeriode}'?",
                    "warning",
                    _viewModel.OnSubmitTaksasiCommand,
                    "Taksasi",
                    "Batal",
                    false,
                    true,
                    "bacameter"
                ), "BacameterRootDialog");
        }
    }
}
