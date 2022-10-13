using System;
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

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitAddAsNormalCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddAsNormalCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var body = new Dictionary<string, dynamic>
            {
                { "KodePeriode", _viewModel.TahunForm.Key + _viewModel.BulanForm.Key },
                { "TglMulaiTagih", ((DateTime)_viewModel.TglMulaiTagihForm).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda1", Convert.ToDateTime(_viewModel.TglDenda1Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda2", Convert.ToDateTime(_viewModel.TglDenda2Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda3",Convert.ToDateTime(_viewModel.TglDenda3Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda4", Convert.ToDateTime(_viewModel.TglDenda4Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDendaPerBulan", Convert.ToDateTime(_viewModel.TglMulaiDendaForm).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "Status", true }
            };

            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    successMessage = response.Data.Ui_msg;
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
            {
                errorMessage = response.Error.Message;
            }

            ShowResult(successMessage, errorMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string errorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(CloseDialog);

                if (errorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowError(errorMessage);
                    });
                }
                else if (successMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowSuccess(successMessage);
                        DialogHost.Close("BillingRootDialog");
                    });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(LoadingDialog);
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            double EstimationTimeInSecond = 80;
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, "sekitar 3-5 menit", false, false, EstimationTimeInSecond), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("BillingRootDialog");
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

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
