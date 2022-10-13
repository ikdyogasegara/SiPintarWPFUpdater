using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnSubmitPengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitPengajuanCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon tunggu", "sedang memproses tindakan...");
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "NomorPengajuanPembelian", _vm.NoDpbPengajuan ?? "" },
                    { "IdGudang", _vm.GudangPengajuan?.IdWilayah ?? 0 },
                    { "TglPengajuan", (_vm.TanggalPengajuan ?? DateTime.Now).ToString("yyyy-MM-dd", new CultureInfo("id-ID")) },
                    { "DiGunakanUntuk", _vm.KeteranganPengajuan ?? "" },
                    { "IdKategoriBarangMasuk", _vm.KategoriPengajuan?.IdKategoriBarangMasuk ?? 0 },
                    { "IdUser", Application.Current?.Resources["IdUser"] ?? 0 },
                };
                var tempDetail = new ObservableCollection<dynamic>();
                foreach (var item in _vm.DaftarBarangPengajuan ?? new())
                {
                    tempDetail.Add(new
                    {
                        idBarang = item.IdBarang,
                        qty = item.TotalQty,
                        stock = item.Stock
                    });
                }
                param.Add("Detail", tempDetail);

                var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/pengajuan-pembelian-barang", param));
                DialogHelper.ShowSnackbar(_isTest, !response.IsError && response.Data.Status ? "success" : "danger", response.Data?.Ui_msg ?? response.Error.Message, "gudang");
                _vm.OnRefreshCommand.Execute(null);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", dg);
            }
            _ = _isTest;
        }
    }
}
