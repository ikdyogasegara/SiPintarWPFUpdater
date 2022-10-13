using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnUnVerifikasiBarangCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnUnVerifikasiBarangCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog",
                "Mohon tunggu", "sedang memproses tindakan...");
            var Param = new Dictionary<string, dynamic>();
            Param.Add("idPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
            var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-unverifikasi", null, Param));
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            if (!Response.IsError)
            {
                DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
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
