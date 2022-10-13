using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnTambahBarangKePengajuanCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnTambahBarangKePengajuanCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var kodeBarang = parameter as string;
            kodeBarang = kodeBarang?.Trim();

            if (!string.IsNullOrWhiteSpace(kodeBarang))
            {
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog",
                    "Mohon tunggu", "sedang memproses tindakan...");

                var barangList = await GetBarangAsync(kodeBarang);
                if (barangList.Count > 0)
                {
                    barangList[0].Stock = await GetBarangStockAsync(barangList[0].IdBarang ?? 0);
                }
                if (barangList.FirstOrDefault(x => x.KodeBarang == kodeBarang) is MasterBarangDto br)
                {
                    if ((_vm.CariBarangForm.Qty
                         + _vm.DataDetailList.FirstOrDefault(x => x.IdBarang == barangList[0].IdBarang)?.Qty ?? decimal.Zero) > barangList[0].Stock)
                    {
                        DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                        await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog",
                            "Tambah Barang Pengajuan", "Stock barang tidak mencukupi!", "error",
                            moduleName: "gudang");
                    }
                    else
                    {
                        var param = new Dictionary<string, dynamic>()
                        {
                            {"idPengajuanPengeluaran", _vm.SelectedData.IdPengajuanPengeluaran},
                            {"idBarang", barangList[0].IdBarang},
                            {"qty", _vm.CariBarangForm.Qty},
                            {"harga_Jual", barangList[0].HargaJual},
                            {"idUser", Application.Current.Resources["IdUser"]?.ToString()},
                        };
                        var response = await Task.Run(() =>
                            _restApi.PostAsync(
                                $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang-detail-tambah-barang", param));
                        DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");

                        if (!response.IsError)
                        {
                            if (response.Data.Status)
                            {
                                DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "gudang");
                                _vm.OnGetDataDetailCommand.Execute(null);
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
                else
                {
                    DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog",
                        "Tambah Barang Pengajuan", "Kode barang tidak ditemukan!", "error",
                        moduleName: "gudang");
                }
            }
            await Task.FromResult(_isTest);
        }

        public async Task<ObservableCollection<MasterBarangWpf>> GetBarangAsync(string kodebarang)
        {
            var param = new Dictionary<string, dynamic>()
            {
                {"IdPdam", Application.Current.Resources["IdPdam"]?.ToString()},
                {"KodeBarang", kodebarang }
            };

            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/master-barang", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    return response.Data.Data.ToObject<ObservableCollection<MasterBarangWpf>>();

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
            return null;
        }

        public async Task<decimal> GetBarangStockAsync(int idBarang)
        {
            var param = new Dictionary<string, dynamic>()
            {
                {"IdPdam", Application.Current.Resources["IdPdam"]?.ToString()},
                {"IdBarang", idBarang },
                {"IdGudang", _vm.SelectedGudang.IdGudang },
            };
            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/master-barang", param));
            if (response.IsError)
            {
                return decimal.Zero;
            }
            if (!response.Data.Status)
            {
                return decimal.Zero;
            }
            var temp = response.Data.Data.ToObject<ObservableCollection<DistribusiBarangStockDetailDto>>();
            if (temp?.Count > 0)
            {
                return temp[0].Qty_Stock ?? decimal.Zero;
            }
            return decimal.Zero;
        }
    }
}
