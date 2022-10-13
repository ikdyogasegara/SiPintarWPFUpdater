using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.Kelainan
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly KelainanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitAddFormCommand(KelainanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            DialogHelper.ShowLoading(_isTest, "BillingRootDialog");

            var body = new Dictionary<string, dynamic>();

            Type type = typeof(MasterKelainanDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormData));
                }
            }

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-kelainan", body));
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

            if (App.OpenedWindow.ContainsKey("billing"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BillingRootDialog", App.OpenedWindow["billing"], true, _viewModel.OnRefreshCommand);

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
