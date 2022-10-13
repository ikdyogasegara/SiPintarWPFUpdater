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
    public class OnGetDataPengajuanDetailCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnGetDataPengajuanDetailCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoadingDetail = true;
            if (!IsTest) { Application.Current.Dispatcher.Invoke(() => { Vm.DataDetail?.Clear(); Vm.DataLpb?.Clear(); }); };
            Vm.IsEmptyDetail = true;
            Vm.IsEmptyLpb = true;

            var Param = new Dictionary<string, dynamic>();
            Param.Add("IdPengajuanPembelian", Vm.SelectedData.IdPengajuanPembelian);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DataDetail = Result.Data.ToObject<ObservableCollection<PengajuanPembelianBarangDetailDto>>();
                    Vm.IsEmptyDetail = Vm.DataDetail != null && Vm.DataDetail?.Count == 0;
                }
                if (!Result.Status)
                {
                    DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }

            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-detail-lpb", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DataLpb = Result.Data.ToObject<ObservableCollection<PengajuanPembelianBarangDiterimaDraftDto>>();
                    Vm.IsEmptyLpb = Vm.DataLpb != null && Vm.DataLpb?.Count == 0;
                }
                if (!Result.Status)
                {
                    DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }

            Vm.IsLoadingDetail = false;
            await Task.FromResult(IsTest);
        }
    }
}
