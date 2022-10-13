using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnSubmitPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitPerbaruiDataRekeningCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new ParamPerbaruiDataRekeningLlttDto
            {
                IdPeriode = _viewModel.PeriodePerbaruiData.IdPeriode,
                IdPelangganLltt = new List<int?> { _viewModel.SelectedData.IdPelangganLltt }
            };

            Type type = typeof(ParamPerbaruiDataRekeningLlttDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(param));
                }
            }

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-lltt-perbarui-data", null, body));
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

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { _ = DialogHost.Show(new GlobalLoadingDialog(null, "Rekening L2T2", "sedang diperbarui"), "BillingRootDialog"); });
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { DialogHost.CloseDialogCommand.Execute(null, null); });

                var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { mainView?.ShowSnackbar(ErrorMessage, "danger"); });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { mainView?.ShowSnackbar(SuccessMessage, "success"); });
                }

                _viewModel.OnRefreshCommand.Execute(null);
            }
        }

    }
}
