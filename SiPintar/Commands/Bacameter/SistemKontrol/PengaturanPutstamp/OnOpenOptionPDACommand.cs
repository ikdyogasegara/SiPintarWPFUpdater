using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Bacameter.SistemKontrol.PengaturanPutstamp;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class OnOpenOptionPDACommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenOptionPDACommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEncryptedPassword = true;

            ShowDialog();

            await ((AsyncCommandBase)_viewModel.GetUserListPDACommand).ExecuteAsync(null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                if (DialogHost.IsDialogOpen("BacameterRootDialog"))
                    DialogHost.Close("BacameterRootDialog");

                _ = DialogHost.Show(new ListUserPDAView(_viewModel), "BacameterRootDialog");
            }
        }
    }
}
