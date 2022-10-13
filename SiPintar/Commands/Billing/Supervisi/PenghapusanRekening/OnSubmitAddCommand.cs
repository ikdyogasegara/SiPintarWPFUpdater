using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnSubmitAddCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddCommand(PenghapusanRekeningViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            CloseDialog();

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var body = new Dictionary<string, dynamic> {
                {"IdPelangganAir", _viewModel.PiutangList.Where(s => s.IsSelected).Select(p => p.IdPelangganAir).FirstOrDefault() },
                {"IdPeriode", _viewModel.PiutangList.Where(s => s.IsSelected).Select(p => p.IdPeriode).ToList() }
            };

            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/hps-scr-akuntansi-rekening-air-add", body));
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

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string successMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    CloseDialog();
                });

                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowError(ErrorMessage);
                    });
                }
                else if (successMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowSuccess(successMessage);
                    });
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
            if (!_isTest)
                _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest)
                DialogHost.Close("BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string errorMessage)
        {
            if (!_isTest)
            {
                var mainView =
                    (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(errorMessage, "danger");

                _viewModel.OnFilterCommand.Execute(null);
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            if (!_isTest)
            {
                var mainView =
                    (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(successMessage, "success");

                _viewModel.OnFilterCommand.Execute(null);
            }
        }
    }
}
