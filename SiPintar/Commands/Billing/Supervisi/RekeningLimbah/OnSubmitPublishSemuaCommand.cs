using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnSubmitPublishSemuaCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitPublishSemuaCommand(RekeningLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var PelangganList = new int[_viewModel.RekeningList.Count];
            int i = 0;
            foreach (var item in _viewModel.RekeningList)
            {
                PelangganList[i] = (int)item.IdPelangganLimbah;
                i++;
            }

            var body = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.PeriodeFilter.IdPeriode },
                { "IdPelangganLimbah", PelangganList },
                { "PasswordUser", _viewModel.ConfirmationPasswordForm }
            };

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-limbah-publish", null, body));
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

            await Task.FromResult(_isTest);
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
            double EstimationTimeInSecond = 60;
            _ = DialogHost.Show(new GlobalLoadingDialog(null, "Mempublish Semua Rek. Limbah", $"{_viewModel.PeriodeFilter.NamaPeriode}", false, false, EstimationTimeInSecond), "BillingRootDialog");
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
