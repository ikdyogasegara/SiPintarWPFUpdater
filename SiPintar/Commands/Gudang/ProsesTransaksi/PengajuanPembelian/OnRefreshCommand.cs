using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _vm.Data = null;
                _vm.IsLoading = true;
                var param = new Dictionary<string, dynamic>();
                if (_vm.HideDone)
                {
                    param.Add("FlagSelesai", !_vm.HideDone);
                }
                if (_vm.FilterNoPengajuanEnabled && _vm.FilterNoPengajuan != null)
                {
                    param.Add("NomorPengajuanPembelian", _vm.FilterNoPengajuan);
                }
                if (_vm.FilterWilayahEnabled && _vm.FilterWilayah != null)
                {
                    param.Add("IdGudang", _vm.FilterWilayah.IdWilayah ?? 0);
                }
                if (_vm.FilterTglPengajuanEnabled && _vm.FilterTglPengajuanMulai != null && _vm.FilterTglPengajuanSampai != null)
                {
                    param.Add("TglPengajuanMulai", (_vm.FilterTglPengajuanMulai ?? DateTime.Now).ToString("yyyy-MM-dd", new CultureInfo("id-ID")));
                    param.Add("TglPengajuanSampaiDengan", (_vm.FilterTglPengajuanSampai ?? DateTime.Now).ToString("yyyy-MM-dd", new CultureInfo("id-ID")));
                }

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pembelian-barang", param);
                if (response != null && response.IsError)
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "gudang", true);
                    return;
                }
                if (response != null && response.Data.Status && response.Data.Data is not null)
                {
                    _vm.Data = response.Data.Data.ToObject<ObservableCollection<PengajuanPembelianBarangDto>>();
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg!, "gudang", true);
                }
                _vm.Data ??= new();
                _vm.Data = new(_vm.Data.OrderByDescending(s => s.TglPengajuan));
            }
            finally
            {
                _vm.IsLoading = false;
            }
            _ = _isTest;
        }
    }
}
