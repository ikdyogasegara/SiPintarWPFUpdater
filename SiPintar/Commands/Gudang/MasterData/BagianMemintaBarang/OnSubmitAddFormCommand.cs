using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly BagianMemintaBarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitAddFormCommand(BagianMemintaBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var _param = parameter as ParamMasterBagianMemintaBarangDto;
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohong tunggu", "sedang menambahkan bagian", "meminta barang...");
            var Param = new Dictionary<string, dynamic>();
            Param.Add("namaBagianMemintaBarang", _param.NamaBagianMemintaBarang);
            Param.Add("idDivisi", _param.IdDivisi);
            Param.Add("idWilayah", _param.IdWilayah);
            Param.Add("namaTtd", _param.NamaTtd);
            Param.Add("jabatanTtd", _param.JabatanTtd);
            Param.Add("nikTtd", _param.NikTtd);
            Param.Add("namaTtd2", _param.NamaTtd2);
            Param.Add("jabatanTtd2", _param.JabatanTtd2);
            Param.Add("nikTtd2", _param.NikTtd2);
            var Response = await Task.Run(() => RestApi.PostAsync($"/api/{ApiVersion}/master-bagian-meminta-barang", Param));
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
