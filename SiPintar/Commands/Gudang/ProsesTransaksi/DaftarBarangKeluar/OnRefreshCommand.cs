using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(DaftarBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.DaftarBarangKeluar = null;
            _vm.DaftarBarangKeluarDetail = null;
            _vm.IsLoading = true;
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _vm.PageSize },
                { "CurrentPage" , _vm.CurrentPage },
            };
            if (_vm.IsCheckedPeriode && _vm.SelectedPeriodeFilter != null && _vm.SelectedPeriodeFilter.IdPeriode != null)
            {
                param.Add("IdPeriode", _vm.SelectedPeriodeFilter.IdPeriode);
            }
            if (_vm.IsCheckedNoTransaksi && !string.IsNullOrWhiteSpace(_vm.NoTransaksiFilter))
            {
                param.Add("NomorTransaksi", _vm.NoTransaksiFilter);
            }
            if (_vm.IsCheckedTglTransaksi && _vm.TglTransaksiAwalFilter.HasValue && _vm.TglTransaksiAkhirFilter.HasValue)
            {
                param.Add("WaktuTransaksiMulai", new DateTime(_vm.TglTransaksiAwalFilter.Value.Year, _vm.TglTransaksiAwalFilter.Value.Month, _vm.TglTransaksiAwalFilter.Value.Day, 0, 0, 0));
                param.Add("WaktuTransaksiSampaiDengan", new DateTime(_vm.TglTransaksiAkhirFilter.Value.Year, _vm.TglTransaksiAkhirFilter.Value.Month, _vm.TglTransaksiAkhirFilter.Value.Day, 23, 59, 59));
            }
            if (_vm.IsCheckedKeterangan && !string.IsNullOrWhiteSpace(_vm.KeteranganFilter))
            {
                param.Add("DiGunakanUntuk", _vm.KeteranganFilter);
            }
            if (_vm.IsCheckedGudang && _vm.SelectedGudangFilter != null && _vm.SelectedGudangFilter.IdGudang != null)
            {
                param.Add("IdGudang", _vm.SelectedGudangFilter.IdGudang);
            }
            if (_vm.IsCheckedStatus && !string.IsNullOrWhiteSpace(_vm.SelectedStatusFilter))
            {
                param.Add("FlagVerifikasiSelesai", _vm.SelectedStatusFilter == "Sudah Verifikasi");
            }
            var resDetail = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/barang-keluar", param);
            if (!resDetail.IsError)
            {
                if (resDetail.Data.Status && resDetail.Data.Data != null)
                {
                    var resData = resDetail.Data.Data.ToObject<ObservableCollection<BarangKeluarDto>>();
                    _vm.DaftarBarangKeluar = resData ?? new ObservableCollection<BarangKeluarDto>();
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", resDetail.Data.Ui_msg, "gudang", true);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", resDetail.Error.Message, "gudang", true);
            }
            _vm.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
