using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPembelian;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnOpenPengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPengajuanCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.WilayahList = null;
            _vm.KategoriBarangMasukList = null;
            _vm.NoDpbPengajuan = null;
            _vm.GudangPengajuan = null;
            _vm.KategoriPengajuan = null;
            _vm.TanggalPengajuan = DateTime.Now;
            _vm.KeteranganPengajuan = null;

            _vm.IsAdd = true;
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);


            var param = new Dictionary<string, dynamic>
            {
                { "TglPengajuan" , (_vm.TanggalPengajuan ?? DateTime.Now).ToString("yyyy-MM-dd", new CultureInfo("id-ID")) }
            };
            _ = _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/pengajuan-pembelian-barang-generate-nomor-pengajuan", param).ContinueWith((result) =>
            {
                var response = result.Result;
                if (!response.IsError && response.Data.Status && response.Data.Data != null)
                {
                    var res = response.Data.Data.ToObject<ObservableCollection<dynamic>>();
                    _vm.NoDpbPengajuan = res?.Count > 0 ? res[0].NomorPengajuanPembelian : null;
                }
            });


            await UpdateListData.ProcessAsync(_isTest, new List<string>
            {
                "MasterWilayah",
                "MasterKategoriBarangMasuk",
            });
            _vm.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahGudangWpf>>();
            _vm.WilayahList ??= new();
            _vm.KategoriBarangMasukList = JArray.FromObject(MasterListGlobal.MasterKategoriBarangMasuk).ToObject<ObservableCollection<MasterKategoriBarangMasukDto>>();
            _vm.KategoriBarangMasukList ??= new();

            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogPengajuanView(_vm);
    }
}
