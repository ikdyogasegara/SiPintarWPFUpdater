using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
    public class OnViewPengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnViewPengajuanCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedData != null)
            {
                _vm.WilayahList = null;
                _vm.KategoriBarangMasukList = null;
                _vm.NoDpbPengajuan = _vm.SelectedData?.NomorPengajuanPembelian;
                _vm.GudangPengajuan = null;
                _vm.KategoriPengajuan = null;
                _vm.TanggalPengajuan = _vm.SelectedData?.TglPengajuan;
                _vm.KeteranganPengajuan = _vm.SelectedData?.DiGunakanUntuk;

                _vm.IsView = true;
                _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);


                await UpdateListData.ProcessAsync(_isTest, new List<string>
                {
                    "MasterWilayah",
                    "MasterKategoriBarangMasuk",
                });
                _vm.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahGudangWpf>>();
                _vm.WilayahList ??= new();
                _vm.KategoriBarangMasukList = JArray.FromObject(MasterListGlobal.MasterKategoriBarangMasuk).ToObject<ObservableCollection<MasterKategoriBarangMasukDto>>();
                _vm.KategoriBarangMasukList ??= new();


                var param = new Dictionary<string, dynamic>
                {
                    { "TglPengajuan" , (_vm.TanggalPengajuan ?? DateTime.Now).ToString("yyyy-MM-dd", new CultureInfo("id-ID")) }
                };
                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/pengajuan-pembelian-barang-generate-nomor-pengajuan", param);
                if (!response.IsError && response.Data.Status && response.Data.Data != null)
                {
                    var res = response.Data.Data.ToObject<ObservableCollection<dynamic>>();
                    _vm.NoDpbPengajuan = res?.Count > 0 ? res[0].NomorPengajuanPembelian : null;
                }

                response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pembelian-barang-detail", new Dictionary<string, dynamic>
                {
                    { "IdPengajuanPembelian", _vm.SelectedData?.IdPengajuanPembelian ?? 0 }
                });
                if (!response.IsError && response.Data.Status)
                {
                    var temp = response.Data.Data.ToObject<ObservableCollection<PengajuanPembelianBarangDetailDto>>();
                    if (temp?.Count > 0)
                    {
                        _vm.KategoriPengajuan = _vm.KategoriBarangMasukList?.FirstOrDefault(S => S.IdKategoriBarangMasuk == temp[0]?.IdKategoriBarangMasuk);

                        _vm.DaftarBarangPengajuan = new();
                        foreach (var item in temp ?? new())
                        {
                            _vm.DaftarBarangPengajuan.Add(new MasterBarangPengajuan()
                            {
                                KodeBarang = item.KodeBarang,
                                NamaBarang = item.NamaBarang,
                                SatuanBarang = item.SatuanBarang,
                                TotalQty = item.Qty,
                                Stock = item.Stock
                            });
                        }
                    }
                }
                _vm.GudangPengajuan = _vm.WilayahList.FirstOrDefault(s => s.IdWilayah == _vm.SelectedData?.IdGudang);
            }
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogPengajuanView(_vm);

    }
}
