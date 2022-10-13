using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Bacameter.SistemKontrol.PengaturanPutstamp;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class OnOpenManualFormPDACommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenManualFormPDACommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm)
                return;

            _viewModel.IsEncryptedPassword = false;
            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                if (DialogHost.IsDialogOpen("BacameterRootDialog"))
                    DialogHost.Close("BacameterRootDialog");

                _ = DialogHost.Show(new ManualPDAView(_viewModel), "BacameterRootDialog");
            }
        }
    }
}
