using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.TipeBarang
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly TipeBarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitEditFormCommand(TipeBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
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
            Param.Add("namaTipeBarang", (string)parameter);
            Param.Add("idTipeBarang", Vm.SelectedData.IdTipeBarang);
            var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/master-tipe-barang", null, Param));
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
