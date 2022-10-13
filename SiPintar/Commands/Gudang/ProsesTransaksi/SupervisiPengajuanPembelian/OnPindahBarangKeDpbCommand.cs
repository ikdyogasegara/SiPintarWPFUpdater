using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnPindahBarangKeDpbCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnPindahBarangKeDpbCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedDataLpb is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Kembalikan ke DPB",
                    "Data belum dipilih!", "error", "OK", false, "gudang");
            }
            else
            {
                object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang memproses data...");
                var Param = new Dictionary<string, dynamic>();
                Param.Add("idPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
                Param.Add("idBarang", Vm.SelectedDataLpb.IdBarang);
                var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail-kembalikan-ke-dpb", null, Param));
                DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
                if (!Response.IsError)
                {
                    if (Response.Data.Status)
                    {
                        Vm.OnGetDataPengajuanDetailCommand.Execute(null);
                    }
                    DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
                }
                else
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
