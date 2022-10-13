using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditCommand(MaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            Type type = typeof(MasterMaterialDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.MaterialForm));
                }
            }

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-material", null, body));
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

            _viewModel.IsLoadingForm = false;

            if (App.OpenedWindow.ContainsKey("perencanaan"))
                DialogHelper.SnackbarPerencanaanHandler(ErrorMessage, SuccessMessage, "error", "success", _isTest, "AtributMaterialDialog", App.OpenedWindow["perencanaan"]);

            _viewModel.OnFilterCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
