using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenLihatVideoCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        private string _dialogIdentifier;

        public OnOpenLihatVideoCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsLoadingForm = true;

            _dialogIdentifier = _viewModel.IsOpenBacaUlang ? "HasilBacaUlangDialog" : "BillingRootDialog";

            DialogHelper.ShowLoading(_isTest, _dialogIdentifier);

            var Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            var Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            var urlFoto = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}V.mp4");
            if (parameter != null)
                urlFoto = parameter as string;

            await ((AsyncCommandBase)_viewModel.GetVideoCommand).ExecuteAsync(urlFoto);

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
            DialogHost.Close(_dialogIdentifier);
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            _ = DialogHost.Show(new VideoView(_viewModel), _dialogIdentifier);
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
                ), _dialogIdentifier);
        }
    }
}
