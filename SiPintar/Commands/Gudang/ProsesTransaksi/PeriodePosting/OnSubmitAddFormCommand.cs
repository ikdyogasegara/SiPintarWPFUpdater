using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitAddFormCommand(PeriodePostingViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
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
                message1: "sedang menambahkan periode..."
            );
            var Param = new Dictionary<string, dynamic>();
            Param.Add("KodePeriode", Convert.ToInt32($"{Vm.TahunPeriode.Value.Key}{Vm.BulanPeriode.Value.Key.ToString().PadLeft(2, '0')}"));
            Param.Add("NamaPeriode", $"{Vm.BulanPeriode.Value.Value} {Vm.TahunPeriode.Value.Value}");
            var Response = await Task.Run(() => RestApi.PostAsync($"/api/{ApiVersion}/master-periode-gudang", Param));
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
