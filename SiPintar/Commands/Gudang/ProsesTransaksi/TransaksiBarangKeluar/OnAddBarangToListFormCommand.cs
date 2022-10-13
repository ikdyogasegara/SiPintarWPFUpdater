﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnAddBarangToListFormCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnAddBarangToListFormCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedGudang is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                    "Pengajuan Pengeluaran Barang", "Gudang belum dipilih!", "warning", moduleName: "gudang");
            }
            else if (_vm.CariBarangForm.Qty <= 0)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                    "Pengajuan Pengeluaran Barang", "Qty tidak boleh kosong!", "warning", moduleName: "gudang");
            }
            else
            {
                _vm.IsLoadingForm = true;
                var kodeBarang = _vm.CariBarangForm.KodeBarang;
                kodeBarang = kodeBarang?.Trim();
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdGudang", _vm.SelectedGudang.IdGudang }
                };
                if (!string.IsNullOrWhiteSpace(kodeBarang))
                {
                    if (_vm.BarangList.FirstOrDefault(x => x.KodeBarang == kodeBarang) is MasterBarangDto br)
                    {
                        var tempData = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf>();
                        if (_vm.Form.DetailWpf != null)
                        {
                            tempData = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf>(_vm.Form.DetailWpf);
                        }
                        var existData = tempData.FirstOrDefault(x => x.KodeBarang == kodeBarang);
                        if (existData == null)
                        {
                            tempData.Add(new ParamPengajuanPengeluaranBarangDetailWpf()
                            {
                                IdBarang = br.IdBarang,
                                KodeBarang = br.KodeBarang,
                                NamaBarang = br.NamaBarang,
                                Satuan = br.SatuanBarang,
                                Harga_Jual = br.HargaJual,
                                Qty = _vm.CariBarangForm.Qty,
                                Wilayah = _vm.SelectedGudang.NamaGudang
                            });
                        }
                        else
                        {
                            existData.Qty = _vm.CariBarangForm.Qty;
                        }
                        _vm.Form.DetailWpf = tempData;
                        _vm.CariBarangForm = new MasterBarangWpf();
                        _vm.InvokeEventOnAddBarangToListPengajuan();
                    }
                    else
                    {
                        await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                            "Pengajuan Pengeluaran Barang", $"Kode barang {kodeBarang} tidak ditemukan",
                            "warning", moduleName: "gudang");
                    }
                }
                else
                {
                    var response = await Task.Run(() =>
                        _restApi.GetAsync(
                            $"/api/{_restApi.GetApiVersion()}/distribusi-barang-stock-barang", param));
                    if (!response.IsError)
                    {
                        if (response.Data.Status)
                        {
                            var res =
                                response.Data.Data.ToObject<ObservableCollection<DistribusiBarangStockDetailDto>>();
                            if (res.Count > 0)
                            {
                                foreach (var item in res)
                                {
                                    var temp = _vm.BarangList.FirstOrDefault(x => x.IdBarang == item.IdBarang);
                                    if (temp != null)
                                    {
                                        temp.Stock = item.Qty_Stock;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "gudang");
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "gudang");
                    }
                }
                _vm.IsLoadingForm = false;
            }

            await Task.FromResult(_isTest);
        }
    }
}
