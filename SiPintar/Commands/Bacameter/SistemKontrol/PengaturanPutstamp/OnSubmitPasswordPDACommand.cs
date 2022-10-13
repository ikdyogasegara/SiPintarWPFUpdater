using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.Hashs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class OnSubmitPasswordPDACommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitPasswordPDACommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Content == null || _viewModel.PasswordForm == null)
                return;

            var PasswordValue = !_viewModel.IsEncryptedPassword ? HashPassword(_viewModel.PasswordForm) : _viewModel.PasswordForm;
            var content = _viewModel.Content;

            if (content.Contains("password_pda", System.StringComparison.CurrentCulture))
            {
                var intercept = new List<string>();
                var perLine = content.Split('\n');
                foreach (var item in perLine)
                {
                    if (item.ToLower().Contains("password_pda"))
                        intercept.Add($"password_pda={PasswordValue}");
                    else
                        intercept.Add(item);
                }
                content = string.Join('\n', intercept.ToArray());
            }
            else
            {
                content += $"\n[setting pda]";
                content += $"\npassword_pda={PasswordValue}";
            }

            _viewModel.Content = content;
            CloseDialog();

            await Task.FromResult(_isTest);
        }

        private string HashPassword(string PlainPassword)
        {
            var result = SecurityHash.EncryptPlainTextToCipherText(PlainPassword);

            Debug.WriteLine("== TEST ENCRYPT ==");
            Debug.WriteLine("Before => " + PlainPassword);
            Debug.WriteLine("After Encrypt => " + result);
            Debug.WriteLine("Try Decrypt => " + SecurityHash.DecryptCipherTextToPlainText(result));

            return result;
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest && DialogHost.IsDialogOpen("BacameterRootDialog"))
                DialogHost.Close("BacameterRootDialog");
        }
    }
}
