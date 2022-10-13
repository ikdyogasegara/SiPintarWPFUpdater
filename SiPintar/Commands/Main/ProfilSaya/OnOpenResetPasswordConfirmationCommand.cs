using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Main.ProfilSaya
{
    public class OnOpenResetPasswordConfirmationCommand : AsyncCommandBase
    {
        private readonly ProfilSayaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordConfirmationCommand(ProfilSayaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.DetailData == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Konfirmasi Reset Kata Sandi",
                    $@"Anda akan mengatur ulang kata sandi milik Anda?",
                    "confirmation",
                    _viewModel.OnOpenResetPasswordFormCommand,
                    "Ganti Kata Sandi",
                    "Batal"
                ), "MainRootDialog");
        }
    }
}
