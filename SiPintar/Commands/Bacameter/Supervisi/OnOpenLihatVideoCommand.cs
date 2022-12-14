using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenLihatVideoCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;
        private string DialogIdentifier;

        public OnOpenLihatVideoCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsLoadingForm = true;

            DialogIdentifier = _viewModel.IsOpenBacaUlang ? "HasilBacaUlangDialog" : "BacameterRootDialog";

            DialogHelper.ShowLoading(_isTest, DialogIdentifier);

            string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            var UrlFoto = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}V.mp4");
            if (parameter != null)
                UrlFoto = parameter as string;

            await ((AsyncCommandBase)_viewModel.GetVideoCommand).ExecuteAsync(UrlFoto);

            _viewModel.IsLoadingForm = false;
            ShowResult();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                if (_viewModel.HasVideo)
                    Application.Current.Dispatcher.Invoke(delegate { ResultDialog(); });
                else
                    ShowAlert();
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close(DialogIdentifier);
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            _ = DialogHost.Show(new VideoView(_viewModel), DialogIdentifier);
        }

        [ExcludeFromCodeCoverage]
        private void ShowAlert()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { AlertDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private void AlertDialog()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Video",
                    "Silakan pilih data lain yang memiliki lampiran video",
                    "error",
                    "oke"
                ), DialogIdentifier);
        }
    }
}
