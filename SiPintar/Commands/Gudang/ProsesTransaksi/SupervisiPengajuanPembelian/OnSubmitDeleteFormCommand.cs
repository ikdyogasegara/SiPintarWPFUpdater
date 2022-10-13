using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitDeleteFormCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog",
                "Mohon tunggu", "sedang menghapus data...");
            string UrlApi = Vm.IsDeletePengajuan ? "supervisi-pengajuan-pembelian-barang-hapus-pengajuan" : "supervisi-pengajuan-pembelian-barang-detail-hapus-barang";
            var Param = new Dictionary<string, dynamic>();
            Param.Add("IdPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
            if (!Vm.IsDeletePengajuan)
            {
                Param.Add("IdBarang", Vm.SelectedDataDetail.IdBarang);
            }
            var Response = await Task.Run(() => RestApi.DeleteAsync($"/api/{ApiVersion}/{UrlApi}", Param));
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            if (!Response.IsError)
            {
                if (Response.Data.Status)
                {
                    DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            if (Vm.IsDeletePengajuan)
            {
                Vm.OnRefreshCommand.Execute(null);
            }
            else
            {
                Vm.OnGetDataPengajuanDetailCommand.Execute(null);
            }
            await Task.FromResult(IsTest);
        }
    }
}
