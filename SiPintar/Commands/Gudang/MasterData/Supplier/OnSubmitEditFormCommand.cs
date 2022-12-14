using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Supplier
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly SupplierViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitEditFormCommand(SupplierViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohong tunggu", "sedang menyimpan perubahan...");
            var Param = new Dictionary<string, dynamic>();
            var _param = parameter as ParamMasterSuplierDto;
            Type type = typeof(ParamMasterSuplierDto);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    Param.Add(property.Name, property.GetValue(_param));
                }
            }
            var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/master-suplier", null, Param));
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Vm.OnRefreshCommand.Execute(null);
            await Task.FromResult(IsTest);
        }
    }
}
