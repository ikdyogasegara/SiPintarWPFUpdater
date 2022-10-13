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
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(PenghapusanRekeningViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            foreach (var i in _viewModel.RekeningAirHapusSecaraAkuntansiList.Where(c => c.IsSelected == true))
            {
                var entityId = new Dictionary<string, dynamic>();
                entityId.Add("IdPelangganAir", i.IdPelangganAir);
                entityId.Add("IdPeriode", i.IdPeriode);

                var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/hps-scr-akuntansi-rekening-air", entityId));
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                        successMessage = response.Data.Ui_msg;
                    else
                        ErrorMessage = response.Data.Ui_msg;
                }
                else
                {
                    ErrorMessage = response.Error.Message;
                }
            }

            ShowResult(successMessage, ErrorMessage);

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
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(ErrorMessage, "danger");

            _viewModel.OnFilterCommand.Execute(null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string successMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(successMessage, "success");

            _viewModel.OnFilterCommand.Execute(null);
        }
    }
}
