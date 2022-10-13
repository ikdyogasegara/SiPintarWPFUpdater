using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnAddBarangPaketToListFormCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnAddBarangPaketToListFormCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
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
            else
            {
                _vm.IsLoadingForm = true;
                if (_vm.SelectedPaketBarang == null)
                {
                    _vm.IsLoadingForm = false;
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                        "Pengajuan Pengeluaran Barang", "Paket belum dipilih!", "warning", moduleName: "gudang");
                }
                else
                {
                    await GetStockUpdateAsync();
                    var param = new Dictionary<string, dynamic>()
                    {
                        { "IdPaketBarang", _vm.SelectedPaketBarang.IdPaketBarang }
                    };
                    var response = await Task.Run(() =>
                        _restApi.GetAsync(
                            $"/api/{_restApi.GetApiVersion()}/master-paket-barang-detail", param));
                    if (!response.IsError)
                    {
                        if (response.Data.Status)
                        {
                            var res =
                                response.Data.Data.ToObject<ObservableCollection<MasterBarangPaketDetailDto>>();
                            if (res?.Count > 0)
                            {
                                await SetDataAsync(res);
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
                    _vm.IsLoadingForm = false;
                }
            }
            await Task.FromResult(_isTest);
        }

        private async Task SetDataAsync(ObservableCollection<MasterBarangPaketDetailDto> res)
        {
            var stokCukup = true;
            foreach (var item in res)
            {
                var br = _vm.BarangList.FirstOrDefault(x => x.KodeBarang == item.KodeBarang);
                if (br == null)
                {
                    stokCukup = false;
                    break;
                }
                stokCukup = item.Qty <= br.Stock;
                if (!stokCukup)
                    break;
            }

            if (stokCukup)
            {
                var tempData = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf>();
                if (_vm.Form.DetailWpf != null)
                {
                    tempData = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf>(_vm.Form.DetailWpf);
                }
                foreach (var item in res)
                {
                    var br = _vm.BarangList.FirstOrDefault(x => x.KodeBarang == item.KodeBarang);
                    if (br == null)
                    {
                        continue;
                    }
                    else
                    {
                        var existData = tempData.FirstOrDefault(x => x.KodeBarang == br.KodeBarang);
                        if (existData == null)
                        {
                            tempData.Add(new ParamPengajuanPengeluaranBarangDetailWpf()
                            {
                                IdBarang = br.IdBarang,
                                KodeBarang = br.KodeBarang,
                                NamaBarang = br.NamaBarang,
                                Satuan = br.SatuanBarang,
                                Harga_Jual = br.HargaJual,
                                Qty = item.Qty,
                                Wilayah = _vm.SelectedGudang.NamaGudang
                            });
                        }
                        else
                        {
                            existData.Qty = item.Qty;
                        }
                    }
                }
                _vm.Form.DetailWpf = tempData;
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                    "Pengajuan Pengeluaran Barang", "Stok barang tidak cukup!", "warning", moduleName: "gudang");
            }
        }

        private async Task GetStockUpdateAsync()
        {
            var param = new Dictionary<string, dynamic>()
            {
                { "IdGudang", _vm.SelectedGudang.IdGudang }
            };
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
    }
}
