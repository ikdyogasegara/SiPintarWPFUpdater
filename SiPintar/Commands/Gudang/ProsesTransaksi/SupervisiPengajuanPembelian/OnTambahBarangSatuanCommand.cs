using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnTambahBarangSatuanCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnTambahBarangSatuanCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.BarangTambahSatuan != null)
            {
                object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "TambahBarangSatuanDialog", "Mohon tunggu", "sedang menambahkan data...");

                RestApiResponse Response = null;
                Dictionary<string, dynamic> ParamSend;
                if (Vm.BarangTambahSatuan.IdBarang is null)
                {
                    ParamSend = new Dictionary<string, dynamic>();
                    ParamSend.Add("KodeBarang", Vm.BarangTambahSatuan.KodeBarang);
                    Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang", ParamSend));
                    if (!Response.IsError)
                    {
                        if (Response.Data.Status && Response.Data.Data != null)
                        {
                            var Data = Response.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                            Vm.BarangTambahSatuan.IdBarang = Data.Count > 0 ? Data[0].IdBarang : null;
                        }
                    }
                    else
                    {
                        DialogHelper.CloseDialog(IsTest, true, "TambahBarangSatuanDialog", dg);
                        await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "TambahBarangSatuanDialog", "Tambah Barang Satuan",
                            $"Barang dengan kode {Vm.BarangTambahSatuan.KodeBarang} tidak ditemukan", "error", "Oke", false, "gudang");
                    }
                }

                if (Vm.BarangTambahSatuan.IdBarang.HasValue)
                {
                    ParamSend = new Dictionary<string, dynamic>();
                    ParamSend.Add("idPengajuanPembelian", Vm.BarangTambahSatuan.IdPengajuanPembelian);
                    ParamSend.Add("idBarang", Vm.BarangTambahSatuan.IdBarang);
                    ParamSend.Add("qty", Vm.BarangTambahSatuan.Qty);
                    ParamSend.Add("stock", Vm.BarangTambahSatuan.Stock ?? 0);
                    Response = await Task.Run(() => RestApi.PostAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail-tambah-barang", ParamSend));
                    DialogHelper.CloseDialog(IsTest, true, "TambahBarangSatuanDialog", dg);
                    DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
                    if (!Response.IsError)
                    {
                        DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
                    }
                    Vm.OnGetDataPengajuanDetailCommand.Execute(null);
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
