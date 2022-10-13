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
    public class OnRefreshCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnRefreshCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            AppDispatcherHelper.Run(IsTest, () =>
            {
                Vm.Data?.Clear();
                Vm.DataDetail?.Clear();
                Vm.DataLpb?.Clear();
            });
            Vm.IsEmpty = true;
            var Param = new Dictionary<string, dynamic>();
            Param.Add("FlagSelesai", false);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang", Param));

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.Data = Result.Data.ToObject<ObservableCollection<PengajuanPembelianBarangDto>>();
                    Vm.IsEmpty = Vm.Data != null && Vm.Data?.Count == 0;
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
            Vm.IsLoading = false;
            await Task.FromResult(IsTest);
        }
    }
}
