using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class OnOpenConfirmationPDACommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenConfirmationPDACommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                if (_viewModel.Content == null || _viewModel.PasswordForm == null)
                    return;

                ShowDialog();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            var Info = _viewModel.IsEncryptedPassword ?
                $"Anda akan menggunakan password milik user '{_viewModel.SelectedUser.NamaUser}' sebagai password user PDA di mobile?"
                : "Anda akan menggunakan password yang di-inputkan sebagai password user PDA di mobile?";

            Info += "\nPassword akan di enkripsi dalam file. Mohon Simpan file ketika selesai.";

            if (!_isTest)
            {
                if (DialogHost.IsDialogOpen("BacameterRootDialog"))
                    DialogHost.Close("BacameterRootDialog");

                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Konfirmasi Password PDA",
                    Info,
                    "confirmation",
                    _viewModel.OnSubmitPasswordPDACommand,
                    "Proses",
                    "Batal",
                    false,
                    false,
                    "bacameter"
                ), "BacameterRootDialog");
            }
        }
    }
}
