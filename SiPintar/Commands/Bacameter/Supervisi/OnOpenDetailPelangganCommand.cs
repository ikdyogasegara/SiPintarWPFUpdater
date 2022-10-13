using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenDetailPelangganCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailPelangganCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            await GetDetailAsync();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetDetailAsync()
        {
            if (!_isTest)
                await ((AsyncCommandBase)_viewModel.OnGetDetailCommand).ExecuteAsync(true);
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { ShowInformation(ErrorMessage); });
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowInformation(string ErrorMessage)
        {
            var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(ErrorMessage, "danger");
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                _ = DialogHost.Show(new DetailPelangganView(_viewModel), "BacameterRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("BacameterRootDialog");
        }
    }
}
