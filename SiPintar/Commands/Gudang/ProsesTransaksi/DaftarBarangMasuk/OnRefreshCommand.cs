using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(DaftarBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.DaftarBarangMasuk = null;
            _vm.DaftarBarangMasukDetail = null;
            _vm.IsLoading = true;
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _vm.PageSize },
                { "CurrentPage" , _vm.CurrentPage },
            };
            if (_vm.IsCheckedPeriode && _vm.SelectedPeriodeFilter != null)
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
            switch (_vm.ActiveTab)
            {
                case DaftarBarangMasukTab.DaftarBarang:
                    var resDetail = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk-detail", param);
                    if (!resDetail.IsError)
                    {
                        if (resDetail.Data.Status)
                        {
                            var resData = resDetail.Data.Data.ToObject<ObservableCollection<BarangMasukDetailDto>>();
                            _vm.DaftarBarangMasukDetail = resData ?? new ObservableCollection<BarangMasukDetailDto>();
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
                    break;
                default:
                    var res = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk", param);
                    if (!res.IsError)
                    {
                        if (res.Data.Status)
                        {
                            var resData = res.Data.Data.ToObject<ObservableCollection<BarangMasukDto>>();
                            _vm.DaftarBarangMasuk = resData ?? new ObservableCollection<BarangMasukDto>();
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", res.Data.Ui_msg, "gudang", true);
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", res.Error.Message, "gudang", true);
                    }
                    break;
            }
            _vm.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
