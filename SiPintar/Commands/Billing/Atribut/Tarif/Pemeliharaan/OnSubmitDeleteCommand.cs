using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Pemeliharaan
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly PemeliharaanViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(PemeliharaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var entityId = new Dictionary<string, dynamic>
            {
                { "IdPemeliharaanLain", _viewModel.SelectedData.IdPemeliharaanLain }
            };

            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/master-tarif-pemeliharaan-lain", entityId));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowResult(SuccessMessage, ErrorMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(SuccessMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { LoadingDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
