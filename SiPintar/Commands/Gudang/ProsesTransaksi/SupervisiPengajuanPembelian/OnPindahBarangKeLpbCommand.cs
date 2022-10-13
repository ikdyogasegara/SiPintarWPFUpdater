using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnPindahBarangKeLpbCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnPindahBarangKeLpbCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedDataDetail is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Masukkan ke LPB",
                    "Data belum dipilih!", "error", "OK", false, "gudang");
            }
            else
            {
                if (!Vm.SelectedDataDetail.FlagVerifikasi ?? false)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Silakan Koreksi & Verifikasi DPB Terlebih Dahulu",
                    $"Barang pada nomor transaksi {Vm.SelectedData.NomorPengajuanPembelian} belum dikoreksi atau diverifikasi. Silakan lakukan proses tersebut untuk melanjutkan tahap selanjutnya.",
                    "error", "Oke", false, "gudang");
                }
                else
                {
                    if ((Vm.SelectedDataDetail.Qty_Sisa_Yang_Diajukan ?? decimal.Zero) > 0)
                    {
                        object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang memproses data...");
                        var Param = new Dictionary<string, dynamic>();
                        Param.Add("idPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
                        Param.Add("idBarang", Vm.SelectedDataDetail.IdBarang);
                        Param.Add("qty", Vm.SelectedDataDetail.Qty);
                        Param.Add("waktuDiterima", System.DateTime.Now);
                        var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail-pindahkan-ke-lpb", null, Param));
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
                    else
                    {
                        await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Masukkan ke LPB",
                    "Qty sudah kosong!", "error", "OK", false, "gudang");
                    }
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
