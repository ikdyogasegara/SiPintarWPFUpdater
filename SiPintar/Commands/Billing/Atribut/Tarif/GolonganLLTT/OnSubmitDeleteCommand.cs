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

namespace SiPintar.Commands.Billing.Atribut.Tarif.GolonganLLTT
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly GolonganLlttViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(GolonganLlttViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            CloseDialog();
            ShowLoading();

            var entityId = new Dictionary<string, dynamic>
            {
                { "IdTarifLltt", _viewModel.SelectedData.IdTarifLltt }
            };

            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/master-tarif-lltt", entityId));
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

            CloseLoadingDialog();

            await ShowDialogAsync(ErrorMessage, SuccessMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    LoadingDialog();
                });
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseLoadingDialog()
        {
            if (!_isTest)
                DialogHost.Close("BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        public async Task ShowDialogAsync(string ErrorMessage, string SuccessMessage)
        {
            if (!_isTest)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (ErrorMessage != null)
                {
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
                }
                else if (SuccessMessage != null)
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(SuccessMessage, "success");
                    _viewModel.OnRefreshCommand.Execute(null);
                }
            }
        }
    }
}
