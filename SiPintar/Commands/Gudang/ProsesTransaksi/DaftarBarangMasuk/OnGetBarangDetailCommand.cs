using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnGetBarangDetailCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetBarangDetailCommand(DaftarBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangMasuk != null)
            {
                _vm.DaftarBarangMasukDetail = null;
                _vm.IsLoadingDetail = true;
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , _vm.PageSize },
                    { "CurrentPage" , 1 },
                    { "IdBarangMasuk" , _vm.SelectedDaftarBarangMasuk.IdBarangMasuk },
                };
                var res = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk-detail", param);
                if (!res.IsError)
                {
                    if (res.Data.Status)
                    {
                        var resData = res.Data.Data.ToObject<ObservableCollection<BarangMasukDetailDto>>();
                        _vm.DaftarBarangMasukDetail = resData ?? new ObservableCollection<BarangMasukDetailDto>();
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
                _vm.IsLoadingDetail = false;
            }
            await Task.FromResult(_isTest);
        }
    }
}
