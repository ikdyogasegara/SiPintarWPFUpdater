using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnGetDetailCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetDetailCommand(DaftarBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangKeluar != null)
            {
                _vm.DaftarBarangKeluarDetail = null!;
                _vm.IsLoadingDetail = true;
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , _vm.PageSize },
                    { "CurrentPage" , _vm.CurrentPage },
                    { "IdBarangKeluar" , _vm.SelectedDaftarBarangKeluar.IdBarangKeluar ?? 0 },
                };

                var resDetail = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/barang-keluar-detail", param);
                if (!resDetail.IsError)
                {
                    if (resDetail.Data.Status && resDetail.Data.Data != null)
                    {
                        var resData = resDetail.Data.Data.ToObject<ObservableCollection<BarangKeluarDetailDto>>();
                        _vm.DaftarBarangKeluarDetail = resData ?? new ObservableCollection<BarangKeluarDetailDto>();
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
                _vm.IsLoadingDetail = false;
            }
            await Task.FromResult(_isTest);
        }
    }
}
