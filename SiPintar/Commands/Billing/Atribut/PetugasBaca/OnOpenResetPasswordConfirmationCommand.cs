using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    public class OnOpenResetPasswordConfirmationCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordConfirmationCommand(PetugasBacaViewModel viewModel, bool isTest = false)
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
                    $"Konfirmasi Ubah Kata Sandi",
                    $@"Anda akan mengatur ulang kata sandi milik '{_viewModel.SelectedData.PetugasBaca}'?",
                    "confirmation",
                    _viewModel.OnOpenResetPasswordFormCommand,
                    "Ubah Kata Sandi",
                    "Batal",
                    false,
                    false,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
