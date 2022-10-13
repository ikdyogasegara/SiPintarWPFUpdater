using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnGenerateNoSpkCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnGenerateNoSpkCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var Param = new Dictionary<string, dynamic>();
            Param.Add("Surat_Perjanjian_Tgl_Laporan", ((DateTime)parameter).ToString("yyyy-MM-ddTHH:mm:ss"));
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/supervisi-pengajuan-pembelian-barang-generate-nomor-surat-perjanjian", Param));
            if (!Response.IsError && Response.Data.Status && Response.Data.Data != null)
            {
                var Res = Response.Data.Data.ToObject<ObservableCollection<dynamic>>();
                Vm.NoSpk = Res.Count > 0 ? Res[0].surat_perjanjian_nomor : null;
            }
            await Task.FromResult(IsTest);
        }
    }
}
