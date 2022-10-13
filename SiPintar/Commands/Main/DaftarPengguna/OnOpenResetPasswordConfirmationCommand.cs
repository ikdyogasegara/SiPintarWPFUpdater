using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnOpenResetPasswordConfirmationCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordConfirmationCommand(DaftarPenggunaViewModel viewModel, bool isTest = false)
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
        private void ShowDialog(MasterUserDto SelectedData)
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Konfirmasi Reset Kata Sandi?",
                    $@"Anda akan mengatur ulang kata sandi pengguna '{SelectedData.Nama}'?",
                    "confirmation",
                    _viewModel.OnOpenResetPasswordFormCommand,
                    "Reset Kata Sandi",
                    "Batal"
                ), "MainRootDialog");
        }
    }
}
