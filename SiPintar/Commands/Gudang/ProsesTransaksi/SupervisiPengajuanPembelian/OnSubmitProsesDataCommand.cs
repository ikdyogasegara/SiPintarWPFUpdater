using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnSubmitProsesDataCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitProsesDataCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "ProsesDataDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "ProsesDataDialog", "Mohon tunggu", "sedang memproses data...");

            var Param = new Dictionary<string, dynamic>();
            PropertyInfo[] prop = typeof(ParamPengajuanPembelianBarangProsesKePersediaanDto).GetProperties();
            foreach (PropertyInfo item in prop)
            {
                if (item.Name != "IdPdam" && item.Name != "IdUserRequest")
                {
                    Param.Add(item.Name, item.GetValue(Vm.ProsesData));
                }
            }

            var Response = await Task.Run(() => RestApi.PostAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-proses-ke-persediaan", Param));
            DialogHelper.CloseDialog(IsTest, true, "ProsesDataDialog", dg);
            if (!Response.IsError)
            {
                DialogHelper.ShowSnackbar(IsTest, Response.Data.Status ? "success" : "danger", Response.Data.Ui_msg, "gudang");
                if (Response.Data.Status)
                {
                    DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
                    Vm.OnRefreshCommand.Execute(null);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
