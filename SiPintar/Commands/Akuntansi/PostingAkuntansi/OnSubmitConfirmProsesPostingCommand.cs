using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;


namespace SiPintar.Commands.Akuntansi.PostingAkuntansi
{
    public class OnSubmitConfirmProsesPostingCommand : AsyncCommandBase
    {
        private readonly PostingAkuntansiViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnSubmitConfirmProsesPostingCommand(PostingAkuntansiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsProsesProgressBar = true;

            ShowResult(successMessage, errorMessage);
            await Task.Delay(3000);

            _viewModel.IsProsesProgressBar = false;

        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                if (errorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowError(errorMessage);
                    });
                }
                else if (successMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(successMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string errorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");

            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
