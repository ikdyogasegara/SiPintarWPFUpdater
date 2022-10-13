using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    public class OnOpenVerifikasiConfirmationCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenVerifikasiConfirmationCommand(VerifikasiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Verifikasi {_viewModel.SelectedData.NamaTipePermohonan} No.Register {_viewModel.SelectedData.NomorPermohonan}",
                    $"Data permohonan yang dipilih akan disetujui dan proses permohonan selesai.",
                    "success",
                    _viewModel.OnSubmitVerifikasiCommand,
                    "Verifikasi",
                    "Batal",
                    false,
                    false,
                    "hublang"
                ), "HublangRootDialog");
        }
    }
}
