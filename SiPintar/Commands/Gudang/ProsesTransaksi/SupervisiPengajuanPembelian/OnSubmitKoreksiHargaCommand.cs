using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnSubmitKoreksiHargaCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitKoreksiHargaCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var Param = parameter as ParamPengajuanPembelianBarangTentukanHargaDto;
            if (Param != null)
            {
                DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
                object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang memproses data...");
                var ParamSend = new Dictionary<string, dynamic>();
                ParamSend.Add("idPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
                ParamSend.Add("idBarang", Param.IdBarang);
                ParamSend.Add("idKategoriBarangMasuk", Param.IdKategoriBarangMasuk);
                ParamSend.Add("qty", Param.Qty);
                ParamSend.Add("harga", Param.Harga);
                var Response = await Task.Run(() => RestApi.PatchAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail-tentukan-harga", null, ParamSend));
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
            await Task.FromResult(IsTest);
        }
    }
}
