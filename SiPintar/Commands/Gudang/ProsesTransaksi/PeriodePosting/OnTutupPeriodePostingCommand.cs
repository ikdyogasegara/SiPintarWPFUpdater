using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnTutupPeriodePostingCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnTutupPeriodePostingCommand(PeriodePostingViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = await DialogHelper.ShowDialogGlobalLoadingAsync(
                isTest: IsTest,
                dispatcher: true,
                identfier: "GudangRootDialog",
                header: "Mohon tunggu",
                message1: "sedang menutup periode..."
            );
            var Param = new Dictionary<string, dynamic>();
            Param.Add("IdPeriode", Vm.SelectedData.IdPeriode);
            Param.Add("Status", false);
            var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/master-periode-gudang", null, Param));
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
            if (!Response.IsError)
            {
                DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Vm.OnRefreshCommand.Execute(null);
            Vm.IsLoading = false;
            await Task.FromResult(IsTest);
        }
    }
}
